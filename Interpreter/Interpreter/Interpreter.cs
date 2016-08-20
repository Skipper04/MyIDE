using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Exceptions;
using Interpreter.Ast;
using Interpreter.Values;
using Interpreter.Interfaces;

namespace Interpreter
{
    public class Interpreter : IInterpreter
    {
        private Ast.Program program;
        private Statement nextStatement;
        private readonly Context context = new Context();
        private readonly string programText;
        private readonly IWriter writer;

        public Interpreter(string programText, IWriter writer)
        {
            if (programText == null || writer == null)
            {
                throw new ArgumentNullException();
            }

            this.programText = programText;
            this.writer = writer;
        }

        public void Interpret()
        {
            try
            {
                nextStatement = program.GetFirstStatement();

                while (nextStatement != null)
                {
                    if (nextStatement is IfStatement)
                    {
                        InterpretIfStatement((IfStatement)nextStatement);
                        continue;
                    }
                    if (nextStatement is ElseStatement)
                    {
                        InterpretElseStatement((ElseStatement)nextStatement);
                        continue;
                    }
                    if (nextStatement is WhileStatement)
                    {
                        InterpretWhileStatement((WhileStatement)nextStatement);
                        continue;
                    }
                    if (nextStatement is ForStatement)
                    {
                        InterpretForStatement((ForStatement)nextStatement);
                        continue;
                    }
                    if (nextStatement is Block)
                    {
                        nextStatement = ((Block)nextStatement).GetFirstStatement();
                        continue;
                    }
                    if (nextStatement is Declaration)
                    {
                        InterpretDeclaration((Declaration)nextStatement);
                        continue;
                    }
                    if (nextStatement is Assignment)
                    {
                        InterpretAssignment((Assignment)nextStatement);
                        continue;
                    }
                    if (nextStatement is Printer)
                    {
                        InterpretPrinter((Printer)nextStatement);
                        continue;
                    }
                    if (nextStatement is Goto)
                    {
                        InterpretGoto((Goto)nextStatement);
                        continue;
                    }

                    throw new InterpreterException(InterpreterException.ExceptionType.UnknownStatement, 
                        nextStatement.Position);
                }
            }
            catch (InterpreterException ex)
            {
                context.Errors.Add(new Error(ex));
            }
        }

        private void InterpretPrinter(Printer printer)
        {
            Value expr;
            try
            {
                expr = printer.Expression.Calculate(context);
                writer.Write(expr.ToString());
                nextStatement = printer.NextStatement;
            }
            catch (ValueException ex)
            {
                context.Errors.Add(new Error(ex, printer.Expression.Position));
                nextStatement = null;
            }
        }

        private void InterpretGoto(Goto @goto)
        {
            nextStatement = @goto.NextStatement;
        }

        private void InterpretAssignment(Assignment assignment)
        {
            if (assignment.Destination is Variable)
            {
                Variable destination = (Variable) assignment.Destination;
                if (!context.VariableValues.ContainsKey(destination.Name))
                {
                    throw new InterpreterException(InterpreterException.ExceptionType.NotDeclaredVariable, assignment.Destination.Position);
                }

                try
                {
                    Value source;
                    try
                    {
                        source = assignment.Source.Calculate(context);
                    }
                    catch (ValueException ex)
                    {
                        context.Errors.Add(new Error(ex, assignment.Source.Position));
                        nextStatement = null;
                        return;
                    }

                    context.VariableValues[destination.Name].Set(source);
                    nextStatement = assignment.NextStatement;
                }
                catch (ValueException ex)
                {
                    context.Errors.Add(new Error(ex, assignment.Position));
                    nextStatement = null;
                }    
            }

            if (assignment.Destination is Slice)
            {
                Slice destination = (Slice) assignment.Destination;
                Value slisedValue = destination.Collection.Calculate(context);
                Value index = destination.Index.Calculate(context);
                
                try
                {
                    Value source;
                    try
                    {
                        source = assignment.Source.Calculate(context);
                    }
                    catch (ValueException ex)
                    {
                        context.Errors.Add(new Error(ex, assignment.Source.Position));
                        nextStatement = null;
                        return;
                    }

                    slisedValue[index] = source;

                    nextStatement = assignment.NextStatement;
                }
                catch (ValueException ex)
                {
                    context.Errors.Add(new Error(ex, assignment.Position));
                    nextStatement = null;
                }
            }
        }

        private void InterpretDeclaration(Declaration declaration)
        {
            if (context.VariableValues.ContainsKey(declaration.Destination.Name))
            {
                throw new InterpreterException(InterpreterException.ExceptionType.AlreadyHasBeenDeclaredVariable,
                    declaration.Destination.Position);
            }

            if (declaration.Source == null)
            {
                context.VariableValues.Add(declaration.Destination.Name,
                    ValueHelper.CreateValue(declaration.Type, declaration.InternalType));
                nextStatement = declaration.NextStatement;
                return;
            }

            try
            {
                context.VariableValues.Add(declaration.Destination.Name, ValueHelper.CreateValue(declaration.Type, declaration.InternalType));
                Value source;
                try
                {
                    source = declaration.Source.Calculate(context);
                }
                catch (ValueException ex)
                {
                    context.Errors.Add(new Error(ex, declaration.Source.Position));
                    nextStatement = null;
                    return;
                }

                context.VariableValues[declaration.Destination.Name].Set(source);
                nextStatement = declaration.NextStatement;
            }
            catch (ValueException ex)
            {
                context.Errors.Add(new Error(ex, declaration.Position));
                nextStatement = null;
            }
        }

        private void InterpretIfStatement(IfStatement ifStatement)
        {
            try
            {
                nextStatement = ifStatement.Condition.Calculate(context).BoolValue
                ? ifStatement.TrueStatement : ifStatement.NextStatement;
            }
            catch (ValueException ex)
            {
                context.Errors.Add(new Error(ex, ifStatement.Condition.Position));
                nextStatement = null;
            }
        }

        private void InterpretElseStatement(ElseStatement elseStatement)
        {
            nextStatement = elseStatement.NextStatement;
        }

        private void InterpretWhileStatement(WhileStatement whileStatement)
        {
            try
            {
                nextStatement = whileStatement.Condition.Calculate(context).BoolValue
                ? whileStatement.TrueStatement : whileStatement.NextStatement;
            }
            catch (ValueException ex)
            {
                context.Errors.Add(new Error(ex, whileStatement.Condition.Position));
                nextStatement = null;
            }

        }

        private void InterpretForStatement(ForStatement forStatement)
        {
            if (!forStatement.IsRun)
            {
                forStatement.IsRun = true;

                nextStatement = forStatement.Initializer;
                return;
            }

            bool forCondition;
            try
            {
                forCondition = forStatement.Condition.Calculate(context).BoolValue;
            }
            catch (ValueException ex)
            {
                context.Errors.Add(new Error(ex, forStatement.Condition.Position));
                nextStatement = null;
                return;
            }

            nextStatement = forCondition ? forStatement.TrueStatement : forStatement.NextStatement;
        }

        public void Build()
        {
            Parser parser = new Parser(programText);
            program = parser.Program();
            context.Errors = parser.GetErrors();
        }

        public void Run()
        {
            Build();
            if (!context.Errors.Any())
            {
                Interpret();
            }
        }

        public List<IError> GetErrors()
        {
            return context.Errors;
        }

        public Dictionary<string, Value> GetVariables()
        {
            return context.VariableValues;
        }
    }
}
