
Pin is reference to specific memory location which can be moved only via unsafe or unpin.
So it types moving reference as unsafe.

Compiler can move reference (box, &) as it wants, so * pointer to same name will change.

Pin makes `& == *` until drop.