

namespace types.recods
module mutually_recursive =

    type Address =
      { Line1: string
        Line2: string
        PostCode: string
        Lives : Person }
    and Person =
      { Name: string
        Age: int
        Address: Address }
