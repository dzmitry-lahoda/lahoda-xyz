
mod one {
    use std::collections::HashMap;

    #[derive(Debug, Clone, Copy, PartialEq, Eq)]
    enum Value {
        Number(i64),
        Boolean(bool),
        Panic(u8, u8),
    }

    enum Parameters {
        Any, // late friday evening, rude hack against type system
    }

    enum Operation<'a> {
        LoadValue(Value),
        WriteVariable(&'a str),
        ReadVariable(&'a str),
        Add,
        Multiply,
        Return,
        LessThan,
        ConditionalJump(u8),
        Call(Vec<Parameters>, Function<'a>),
        Panic,
    }

    struct Function<'a> {
        ops: Vec<Operation<'a>>,
    }

    impl<'a> Function<'a> {
        pub fn statement(&mut self, op: Operation<'a>) -> u8 {
            let len = self.ops.len();
            self.ops.push(op);
            len as u8
        }
    }

    struct Interpreter {
        variables: HashMap<String, Value>,
        return_value: Option<Value>,
        stack: Vec<Value>,
    }

    impl Interpreter {
        pub fn run(&mut self, f: &Function, debug: bool, depth: u8) {
            let mut line = 0;
            while line < f.ops.len() && self.return_value.is_none() {
                let op = &f.ops[line];
                line += 1;
                match op {
                    Operation::LoadValue(x) => self.stack.push(*x),
                    Operation::WriteVariable(x) => {
                        self.variables
                            .insert(x.to_string(), self.stack.pop().expect("value on the stack"));
                    }
                    Operation::ReadVariable(x) => {
                        self.stack.push(
                            *(self
                                .variables
                                .get(*x)
                                .expect("can read only declared variables")),
                        );
                    }
                    Operation::Add => match (self.stack.pop(), self.stack.pop()) {
                        (Some(Value::Number(a)), Some(Value::Number(b))) => {
                            self.stack.push(Value::Number(a + b));
                        }
                        _ => panic!("add needs 2 variables on the stack"),
                    },
                    Operation::Multiply => match (self.stack.pop(), self.stack.pop()) {
                        (Some(Value::Number(a)), Some(Value::Number(b))) => {
                            self.stack.push(Value::Number(a * b));
                        }
                        _ => panic!("multiply needs 2 variables on the stack"),
                    },
                    Operation::Return => {
                        self.return_value =
                            Some(self.stack.pop().expect("return can return only from stack"))
                    }
                    Operation::LessThan => match (self.stack.pop(), self.stack.pop()) {
                        (Some(Value::Number(a)), Some(Value::Number(b))) => {
                            self.stack.push(Value::Boolean(a > b));
                        }
                        _ => panic!("multiply needs 2 variables on the stack"),
                    },
                    Operation::ConditionalJump(goto) => match self.stack.pop() {
                        Some(Value::Boolean(value)) => {
                            if value {
                                if debug {
                                    println!("jump to {:?}", goto);
                                }
                                line = *goto as usize;
                            }
                        }
                        other => panic!(
                            "Conditional jump can operate only on booleans, but got {:?}",
                            other
                        ),
                    },
                    Operation::Call(parameters, function) => {
                        let mut call = Interpreter {
                            return_value: None,
                            stack: vec![],
                            variables: HashMap::new(),
                        };
                        for _ in parameters {
                            call.stack
                                .push(self.stack.pop().expect("function call needs parameters"));
                        }
                        call.run(function, debug, depth + 1);

                        self.stack
                            .push(call.return_value.expect("functions always return"));

                        if matches!(self.stack.last(), Some(Value::Panic(_, _))) {
                            // wow, i can return ops/stack here
                            self.return_value = self.stack.pop();
                            break;
                        }
                    }
                    Operation::Panic => {
                        self.return_value = Some(Value::Panic(depth, (line - 1) as u8));
                        break;
                    }
                }
            }
        }
    }

    #[cfg(test)]
    mod tests {
        use super::*;

        #[test]
        fn simple_math() {
            let mut function = Function { ops: Vec::new() };
            function.statement(Operation::LoadValue(Value::Number(1)));
            function.statement(Operation::WriteVariable("x"));
            function.statement(Operation::LoadValue(Value::Number(2)));
            function.statement(Operation::WriteVariable("y"));
            function.statement(Operation::ReadVariable("x"));
            function.statement(Operation::LoadValue(Value::Number(1)));
            function.statement(Operation::Add);
            function.statement(Operation::ReadVariable("y"));
            function.statement(Operation::Multiply);
            function.statement(Operation::Return);
            let mut interpreter = Interpreter {
                stack: Vec::new(),
                variables: HashMap::new(),
                return_value: None,
            };
            interpreter.run(&function, false, 0);

            assert!(matches!(interpreter.return_value, Some(Value::Number(4))))
        }

        #[test]
        fn fib() {
            let mut add = Function { ops: Vec::new() };
            add.statement(Operation::Add);
            add.statement(Operation::Return);

            let mut function = Function { ops: Vec::new() };

            function.statement(Operation::LoadValue(Value::Number(2)));
            function.statement(Operation::LoadValue(Value::Number(3)));
            function.statement(Operation::Call(vec![Parameters::Any, Parameters::Any], add));
            function.statement(Operation::LoadValue(Value::Number(3)));
            function.statement(Operation::Add);
            function.statement(Operation::Return);

            let mut interpreter = Interpreter {
                stack: Vec::new(),
                variables: HashMap::new(),
                return_value: None,
            };
            interpreter.run(&function, false, 0);
            assert!(matches!(interpreter.return_value, Some(Value::Number(8))))
        }

        #[test]
        fn exception() {
            let mut add = Function { ops: Vec::new() };
            add.statement(Operation::Panic);
            add.statement(Operation::Return);

            let mut function = Function { ops: Vec::new() };

            function.statement(Operation::LoadValue(Value::Number(2)));
            function.statement(Operation::LoadValue(Value::Number(3)));
            function.statement(Operation::Call(vec![Parameters::Any, Parameters::Any], add));
            function.statement(Operation::LoadValue(Value::Number(3)));
            function.statement(Operation::Add);
            function.statement(Operation::Return);

            let mut interpreter = Interpreter {
                stack: Vec::new(),
                variables: HashMap::new(),
                return_value: None,
            };
            interpreter.run(&function, false, 0);
            assert!(matches!(interpreter.return_value, Some(Value::Panic(1, 0))))
        }

        #[test]
        fn less_than() {
            let mut function = Function { ops: Vec::new() };

            function.statement(Operation::LoadValue(Value::Number(1)));
            function.statement(Operation::LoadValue(Value::Number(5)));
            function.statement(Operation::LessThan);
            function.statement(Operation::Return);

            let mut interpreter = Interpreter {
                stack: Vec::new(),
                variables: HashMap::new(),
                return_value: None,
            };
            interpreter.run(&function, false, 0);
            assert!(matches!(
                interpreter.return_value,
                Some(Value::Boolean(true))
            ))
        }

        #[test]
        fn sum_five() {
            let mut function = Function { ops: Vec::new() };

            function.statement(Operation::LoadValue(Value::Number(0)));
            function.statement(Operation::WriteVariable("counter"));

            function.statement(Operation::LoadValue(Value::Number(0)));
            function.statement(Operation::WriteVariable("sum"));

            // sum+=2
            let loop_line = function.statement(Operation::ReadVariable("sum"));
            function.statement(Operation::LoadValue(Value::Number(2)));
            function.statement(Operation::Add);
            function.statement(Operation::WriteVariable("sum"));

            // counter+=1
            function.statement(Operation::ReadVariable("counter"));
            function.statement(Operation::LoadValue(Value::Number(1)));
            function.statement(Operation::Add);
            function.statement(Operation::WriteVariable("counter"));

            function.statement(Operation::ReadVariable("counter"));
            function.statement(Operation::LoadValue(Value::Number(5)));
            function.statement(Operation::LessThan);
            function.statement(Operation::ConditionalJump(loop_line));

            function.statement(Operation::ReadVariable("sum"));
            function.statement(Operation::Return);

            let mut interpreter = Interpreter {
                stack: Vec::new(),
                variables: HashMap::new(),
                return_value: None,
            };
            interpreter.run(&function, false, 0);

            assert!(matches!(interpreter.return_value, Some(Value::Number(10))))
        }
    }
}
