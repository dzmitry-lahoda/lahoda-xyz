const std = @import("std");
pub fn compound_interest(years: u16, percentage: f32, base: f32) f32 {
    const yearsFloat: f32 = @floatFromInt(years);
    return base * std.math.pow(f32, 1.0 + percentage, yearsFloat);
}
pub fn apr(dayli: f32) f32 {
    return dayli * 365.0;
}
pub fn main() void {
    const stdout = std.io.getStdOut().writer();
    const twelve: f32 = compound_interest(12, 0.06, 100);
    stdout.print("{}\n", .{twelve}) catch {};
    const apr_val: f32 = compound_interest(365, 0.000627468, 100);
    stdout.print("{}\n", .{apr_val}) catch {};
}
