
type Price = u64;

#[derive(Clone)]
struct Market {
    index_price: Price,
}

struct Order {
    price: Price,
    size: i64,
}

struct OrderBook {
    bids: btree_slab::BTreeMap<Price, Order>,
    asks: Vec<Order>,
}

pub fn add(left: usize, right: usize) -> usize {
    left + right
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn it_works() {
        let result = add(2, 2);
        assert_eq!(result, 4);
    }
}
