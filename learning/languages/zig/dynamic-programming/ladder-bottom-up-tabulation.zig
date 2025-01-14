// is like fibonatti but for 3 elements
// i have ladder of n steps,
// i can take 1, 2 or 3 steps at a time
// how many ways can i climb the ladder
const assert = @import("std").debug.assert;

pub fn ladder(n: u32) u32 {
    var a1: u32 = 1;
    var a2: u32 = 2;
    var a3: u32 = 4;

    if (n == 0) {
        return 0;
    } else if (n == 1) {
        return a1;
    } else if (n == 2) {
        return a2;
    } else if (n == 3) {
        return a3;
    }
    var i: u32 = n;
    while (i > 3) : (i -= 1) {
        const an = a1 + a2 + a3;
        a1 = a2;
        a2 = a3;
        a3 = an;
    }
    return a3;
}

test "ladder" {
    assert(ladder(0) == 0);
    assert(ladder(1) == 1);
    assert(ladder(2) == 2);
    assert(ladder(3) == 4);
    assert(ladder(4) == 7);
    assert(ladder(5) == 13);
}
