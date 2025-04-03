//zig function with main...

const std = @import("std");

pub fn compound_interest(years: u8, percentage: f32, base: f32) f32 {
    const yearsFloat: f32 = @floatFromInt(years);
    return base * std.math.pow(f32, 1.0 + percentage, yearsFloat);
}
pub fn main() void {
    const stdout = std.io.getStdOut().writer();
    const twelve: f32 = compound_interest(12, 0.06, 100);
    // print f32 twelve value
    stdout.print("{}", .{twelve}) catch {};

    //
}
