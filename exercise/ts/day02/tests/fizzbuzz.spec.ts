import { parametrableFizzbuzz, max, min } from '../src/fizzbuzz';
import * as O from 'fp-ts/Option';
import { isNone, isSome } from 'fp-ts/Option';
import * as fc from 'fast-check';
import { pipe } from 'fp-ts/function';

let mapping: Map<number, string> = new Map([
    [15, 'FizzBuzz'],
    [3, 'Fizz'],
    [5, 'Buzz'],
]);

var fizzBuzz = parametrableFizzbuzz(mapping)

var whizzBang = parametrableFizzbuzz(new Map([
    [7, "Whizz"],
    [11, "Bang"]
]))


describe('WhizzBang should return', () => {
    test.each([
        [7, "Whizz"],
        [8, "8"],
        [11, "Bang"],
        [14, "Whizz"],
        [22, "Bang"],
        [77, "Whizz"],
        [88, "Bang"],
        [55, "Bang"]
    ])('its representation %s -> %s', (input, expectedResult) => {
        const conversionResult = whizzBang(input);
        expect(isSome(conversionResult)).toBeTruthy();

        if (isSome(conversionResult)) {
            expect(conversionResult.value).toBe(expectedResult);
        }
    });
})
describe('FizzBuzz should return', () => {
    test.each([
        [1, '1'],
        [67, '67'],
        [82, '82'],
        [3, 'Fizz'],
        [66, 'Fizz'],
        [99, 'Fizz'],
        [5, 'Buzz'],
        [50, 'Buzz'],
        [85, 'Buzz'],
        [15, 'FizzBuzz'],
        [30, 'FizzBuzz'],
        [45, 'FizzBuzz']
    ])('its representation %s -> %s', (input, expectedResult) => {
        const conversionResult = fizzBuzz(input);
        expect(isSome(conversionResult)).toBeTruthy();

        if (isSome(conversionResult)) {
            expect(conversionResult.value).toBe(expectedResult);
        }
    });

    test('valid strings for numbers between 1 and 100', () => {
        fc.assert(
            fc.property(
                fc.integer().filter(n => n >= min && n <= max),
                (n) => isConvertValid(n)
            )
        );
    });

    const isConvertValid = (input: number): boolean => pipe(
        fizzBuzz(input),
        O.exists(result => validStringsFor(input).includes(result))
    );

    const validStringsFor = (x: number): string[] => ['Fizz', 'Buzz', 'FizzBuzz', x.toString()];

    test('none for numbers out of range', () => {
        fc.assert(
            fc.property(
                fc.integer().filter(n => n < min || n > max),
                (n) => isNone(fizzBuzz(n))
            )
        );
    });
});