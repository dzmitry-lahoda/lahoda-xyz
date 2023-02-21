import { retries, suite, test } from '@testdeck/mocha';
import { expect, assert } from "chai";
import { Eq, struct, contramap } from 'fp-ts/lib/Eq';
import { fromCompare, Ord, contramap as cmo } from 'fp-ts/lib/Ord';
import { getFunctionSemigroup, min as minSG, Semigroup, semigroupAll, struct as structSG} from 'fp-ts/lib/Semigroup';
import { fold, getEq, ReadonlyNonEmptyArray, concatAll } from 'fp-ts/lib/ReadonlyNonEmptyArray';
import { semigroup } from 'fp-ts';
import { SemigroupSum } from 'fp-ts/lib/number';
import { getSemigroup } from 'fp-ts/lib/These';
import { SemigroupAll } from 'fp-ts/lib/boolean';
import { flap, Functor1 } from 'fp-ts/lib/Functor';
import { Kind, URIS, URItoKind } from 'fp-ts/HKT';

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

interface A0<A0> {}
interface A1<A0, A1> {}
interface A2<A0, A1, A2> {}
interface A3<A0, A1, A2, A3> {}

export const URIC = "Foo"
export type URIZ = typeof URIC

interface DeclarationMergingExampleInterface {
    a: Number
}
interface DeclarationMergingExampleInterface {
    b: String
}

type Foo<A0, A1> = {
    foobar : BigInt
    a: A0,
    b: A1,
}

interface A1<A0, A1> {
    readonly [URIC]: Foo<A0, A1>
}


type MyResult = A1<string, number>["Foo"]

export type AIS  = keyof A1<any, any>

export type KK<URIZ extends AIS, E, A> = URIZ extends AIS ? A1<E, A> : any

type FooBar = KK<"Foo", string, number>

export interface FFF<F extends AIS> {
    readonly URI : F
    readonly map : <E, A, B>(fa: KK<F, E,A>, f: (a:A) => B) => KK<F, E, B>
}

export const functorE: FFF<"Foo"> = {
    URI: 'Foo',
    map: function <E, A, B>(fa: A1<E, A>, f: (a: A) => B): A1<E, B> {
        // let x: FooBar = {
        //     [URIC]: {
        //         foobar: undefined,
        //         a: undefined,
        //         b: undefined
        //     }
        // }
        return {
            ["Foo"] : {
                foobar: BigInt(42),
                a: fa["Foo"].a,
                b : f(fa["Foo"].b),
            }

        }
    }
}


// function addOne<URI extends URIS>(F: Functor1<URI>) {
//     return (fa: Kind<F, number>): Kind<F, number> => F.map(fa, (n) => n + 1)
//   }

type Fish = { swim: () => void };
type Bird = { fly: () => void };
@suite class SdkUnitTests {
    @test "basic ts"() {
        functorE
        const a: number | undefined = undefined;
        assert(!a); 
        let x: Fish = { swim : () => console.log(42)}
        if ("swim" in x) {
            x.swim()
        } 
    
        function xxx(a: never) {
            const xx: Number = a;
        }
    }

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
