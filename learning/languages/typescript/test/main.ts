import { suite, test } from '@testdeck/mocha';
import { expect, assert } from "chai";
import { Eq, getStructEq, contramap } from 'fp-ts/lib/Eq';
import { getEq, ReadonlyNonEmptyArray } from 'fp-ts/lib/ReadonlyNonEmptyArray';

// interface A {
//     b: string;
// }

// interface Eq<A> {
//     readonly equals: (x: A, y: A) => boolean
// }

const eqNumber: Eq<number> = {
    equals: (x, y) => x === y
}

function elem<A>(E: Eq<A>): (a: A, as: Array<A>) => boolean {
    return (a, as) => as.some(item => E.equals(item, a))
}

type Point = {
    x: number
    y: number
}

const eqPoint: Eq<Point> = getStructEq(
    {
        x: eqNumber,
        y: eqNumber
    }
)

export const eqArrayOfPoints : Eq<ReadonlyNonEmptyArray<Point>> = getEq(eqPoint);


type Item = {
    id: number,
    displayName: string,
}

const eqItem : Eq<Item> = contramap((x: Item) => x.id )(eqNumber);

@suite class SdkUnitTests {

    @test "fp"() {
        assert(elem(eqNumber)(42, [1, 2, 3, 42]))

    }

    @test 'builder type'() {
        interface Cosmos {
            _cosmos: "cosmos";
        };
        interface Composable {
            _composable: "composable";
        };
        type Network = Cosmos | Composable;
        interface X<T = Network> {
            x: T
        }

        let a: X<Cosmos> = { x: { _cosmos: "cosmos" } };
        let b: X<Composable> = { x: { _composable: "composable" } };
        // NOTE: will fail to compile, so any other way using class/enum/or without data passed compilation fine
        //a = b ;
    }
}
