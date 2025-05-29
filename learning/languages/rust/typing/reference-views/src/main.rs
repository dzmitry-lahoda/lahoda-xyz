struct Abc {
    a: i32,
    b: String,
    c: Vec<u8>,
}

struct Abc1<'a> {
    a: &'a i32,
    b: &'a String,
    c: &'a Vec<u8>,
}

struct Abc2<'a> {
    a: &'a mut i32,
    b: &'a mut String,
    c: &'a mut Vec<u8>,
}

struct Abc3<'a> {
    a: &'a mut i32,
    b: &'a String,
    c: &'a mut Vec<u8>,
}

struct Abc11<'a> {
    a: &'a mut i32,
    b: &'a String,
    c: &'a Vec<u8>,
}

impl<'a> Abc11<'a> {
    pub fn from_abc(abc: &'a mut Abc) -> Self {
        Abc11 {
            a: &mut abc.a,
            b: &abc.b,
            c: &abc.c,
        }
    }
}

struct Bc1<'a> {
    b: &'a String,
    c: &'a Vec<u8>,
}

impl<'a> Bc1<'a> {
    pub fn from_abc(abc: &'a Abc) -> Self {
        Bc1 {
            b: &abc.b,
            c: &abc.c,
        }
    }
}

impl Abc {
    pub fn to_bc_and_abc11<'a>(&'a mut self) -> (Bc1<'a>, Abc11<'a>) {
        (
            Bc1 {
                b: &self.b,
                c: &self.c,
            },
            Abc11 {
                a: &mut self.a,
                b: &self.b,
                c: &self.c,
            },
        )
    }
}

struct Abc4<'a, 'b, 'c> {
    a: &'a mut i32,
    b: &'b String,
    c: &'c mut Vec<u8>,
}

impl<'a> Abc1<'a> {
    pub fn from_abc(abc: &'a Abc) -> Self {
        Abc1 {
            a: &abc.a,
            b: &abc.b,
            c: &abc.c,
        }
    }
}

impl<'a> Abc2<'a> {
    pub fn from_abc(abc: &'a mut Abc) -> Self {
        Abc2 {
            a: &mut abc.a,
            b: &mut abc.b,
            c: &mut abc.c,
        }
    }
}

impl<'a> Abc3<'a> {
    pub fn from_abc(abc: &'a mut Abc) -> Self {
        Abc3 {
            a: &mut abc.a,
            b: &abc.b,
            c: &mut abc.c,
        }
    }
}

impl<'a, 'b, 'c> Abc4<'a, 'b, 'c> {
    pub fn from_abc<'abc>(abc: &'abc mut Abc) -> Self
    where
        'abc: 'a,
        'abc: 'b,
        'abc: 'c,
    {
        Abc4 {
            a: &mut abc.a,
            b: &abc.b,
            c: &mut abc.c,
        }
    }
}

fn main() {
    println!("Hello, world!");
}
