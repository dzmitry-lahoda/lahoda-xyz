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
