-- deserealizaiton only parts


def deserialize_u8 (stream: IO.FS.Stream) : IO (Option UInt8) := do
  let b ← stream.read 1
  pure b[0]?


-- TODO: how to read from some kind of slice/iterator, not hardcoded dep to stream?
partial def deserialize_u16 (stream:  IO.FS.Stream) : IO (Option UInt16) := do
  let data ← stream.read 2
  let less := data[0]?
  let more := data[1]?
  let result := match less, more with
    -- TODO: does not feels efficient to convert into 2 u16, better do it once and then just bit shift it
    | some less, some more => some (less.toUInt16 + more.toUInt16 * 256)
    | _, _ => none

  pure result

def Int8 := Int
def Int16 := Int
def Int32 := Int
def Int64 := Int
def Uint128 := Nat
def Int128 := Int

structure Sums where
  bool: Option Bool
  primitives: PrimitivesSums

inductive PrimitivesSums where
  | bool: Bool →  PrimitivesSums
  | u8: UInt8 → PrimitivesSums

structure Primitives where
  u8: UInt8
  u16: UInt16
  u32: UInt32
  u64: UInt64
  i8:  Int8
  i16: Int16
  i32: Int32
  i64: Int64
  u128: Uint128
  i128: Int128
  bool: Bool

structure Containers where
  primitive: List UInt8
  stuctures : List Primitives
  string : String
  tuple: (String × UInt8)
