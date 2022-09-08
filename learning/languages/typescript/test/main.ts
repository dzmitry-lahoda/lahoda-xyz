import { suite, test } from '@testdeck/mocha';
import { expect, assert } from "chai";
import { Eq, struct, contramap } from 'fp-ts/lib/Eq';
import { fromCompare, Ord, contramap as cmo } from 'fp-ts/lib/Ord';
import { getFunctionSemigroup, min as minSG, Semigroup, semigroupAll, struct as structSG} from 'fp-ts/lib/Semigroup';
import { fold, getEq, ReadonlyNonEmptyArray, concatAll } from 'fp-ts/lib/ReadonlyNonEmptyArray';
import { semigroup } from 'fp-ts';
import { SemigroupSum } from 'fp-ts/lib/number';
import { getSemigroup } from 'fp-ts/lib/These';
import { SemigroupAll } from 'fp-ts/lib/boolean';

// interface A {
//     b: string;
// }

// interface Eq<A> {
//     readonly equals: (x: A, y: A) => boolean
// }

const eqNumber: Eq<number> = {
    equals: (x, y) => x === y
}

const ordNumber: Ord<number> = fromCompare((x, y) => (x < y ? -1 : x > y ? 1 : 0));

function min<A>(O: Ord<A>): (x: A, y: A) => A {
    return (x, y) => O.compare(x, y) === 1 ? y : x
}

function elem<A>(E: Eq<A>): (a: A, as: Array<A>) => boolean {
    return (a, as) => as.some(item => E.equals(item, a))
}

type Point = {
    x: number
    y: number
}

const eqPoint: Eq<Point> = struct(
    {
        x: eqNumber,
        y: eqNumber
    }
)

export const eqArrayOfPoints: Eq<ReadonlyNonEmptyArray<Point>> = getEq(eqPoint);


type Item = {
    id: number,
    displayName: string,
    age: number,
}


function getLastSemigroup<A = never>(): Semigroup<A> {
    return { concat: (x, y) => y }
}

const semigroupString: Semigroup<string> = {
    concat : (x, y) => x + y
}

const semigroupPoint: Semigroup<Point> = structSG (
    {
        x: SemigroupSum,
        y: SemigroupSum,
    }
)

const semigroupMin: Semigroup<number> = minSG(ordNumber);

const semigroupPredicate : Semigroup<(p:Point) => boolean> = getFunctionSemigroup(SemigroupAll)<Point>()


const eqItem: Eq<Item> = contramap((x: Item) => x.id)(eqNumber);

const byAge: Ord<Item> = cmo((x: Item) => x.age)(ordNumber);

@suite class SdkUnitTests {

    @test "fp"() {
        assert(elem(eqNumber)(42, [1, 2, 3, 42]))
        expect(min(ordNumber)(42, 3)).to.be.equal(3)
        const x = min(byAge)
        semigroupMin.concat(1,2)
        const isPositiveX = (p: Point) : boolean => p.x > 0;
        const isPositiveY = (p: Point) : boolean => p.y > 0;
        const xy = semigroupPredicate.concat(isPositiveX, isPositiveY);
        xy({x : 1, y: 2})
        xy({x : 1, y: 2})
        xy({x : -1, y: 3})
        const minFold = concatAll(semigroupMin);
        expect(minFold([33,3,4,5])).to.be.eq(3);
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
