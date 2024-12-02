import {none, Option, some} from "fp-ts/Option";

export const min = 1;
export const max = 100;


export const parametrableFizzbuzz = (mapping: Map<number, string>) => (input: number): Option<string> =>
    isOutOfRange(input)
        ? none
        : some(convertSafely(input, mapping));

function convertSafely(input: number, mapping: Map<number, string>): string {
    for (const [divisor, value] of mapping) {
        if (is(divisor, input)) {
            return value;
        }
    }
    return input.toString();
}

const is = (divisor: number, input: number): boolean => input % divisor === 0;
const isOutOfRange = (input: number): boolean => input < min || input > max;