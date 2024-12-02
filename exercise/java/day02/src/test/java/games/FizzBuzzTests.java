package games;

import io.vavr.collection.LinkedHashMap;
import io.vavr.collection.Seq;
import io.vavr.test.Arbitrary;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import java.util.stream.Stream;

import static games.FizzBuzz.MAX;
import static games.FizzBuzz.MIN;
import static io.vavr.API.List;
import static io.vavr.API.Some;
import static io.vavr.test.Arbitrary.integer;
import static io.vavr.test.Property.def;
import static org.assertj.core.api.Assertions.assertThat;

class FizzBuzzTests {
    private static final Seq<String> fizzBuzzStrings = List("Fizz", "Buzz", "FizzBuzz");

    private static final FizzBuzz fizzBuzz = new FizzBuzz(LinkedHashMap.of(
            15, "FizzBuzz",
            3, "Fizz",
            5, "Buzz"
    ));

    private static final FizzBuzz whizzBang = new FizzBuzz(LinkedHashMap.of(
            7, "Whizz",
            11, "Bang"
    ));

    public static Stream<Arguments> fizzBuzzValidInputs() {
        return Stream.of(
                Arguments.of(1, "1"),
                Arguments.of(67, "67"),
                Arguments.of(82, "82"),
                Arguments.of(3, "Fizz"),
                Arguments.of(66, "Fizz"),
                Arguments.of(99, "Fizz"),
                Arguments.of(5, "Buzz"),
                Arguments.of(50, "Buzz"),
                Arguments.of(85, "Buzz"),
                Arguments.of(15, "FizzBuzz"),
                Arguments.of(30, "FizzBuzz"),
                Arguments.of(45, "FizzBuzz")
        );
    }

    public static Stream<Arguments> whizzBangValidInputs() {
        return Stream.of(
                Arguments.of(7, "Whizz"        ),
                Arguments.of(8, "8"     ),
                Arguments.of(11, "Bang" ),
                Arguments.of(14, "Whizz" ),
                Arguments.of(22, "Bang"),
                Arguments.of(77, "Whizz"         ),
                Arguments.of(88, "Bang"),
                Arguments.of(55, "Bang")
        );
    }

    @ParameterizedTest
    @MethodSource("fizzBuzzValidInputs")
    void parse_successfully_numbers_between_1_and_100_samples_using_fizzBuzz(int input, String expectedResult) {
        assertThat(fizzBuzz.convert(input))
                .isEqualTo(Some(expectedResult));
    }

    @ParameterizedTest
    @MethodSource("whizzBangValidInputs")
    void parse_successfully_numbers_between_1_and_100_samples_using_whizzBang(int input, String expectedResult) {
        assertThat(whizzBang.convert(input))
                .isEqualTo(Some(expectedResult));
    }


    @Test
    void parse_return_valid_string_for_numbers_between_1_and_100() {
        def("Some(validString) for numbers in [1; 100]")
                .forAll(validInput())
                .suchThat(this::isConvertValid)
                .check()
                .assertIsSatisfied();
    }

    @Test
    void parse_fail_for_numbers_out_of_range() {
        def("None for numbers out of range")
                .forAll(invalidInput())
                .suchThat(x -> fizzBuzz.convert(x).isEmpty())
                .check()
                .assertIsSatisfied();
    }

    private boolean isConvertValid(Integer x) {
        return fizzBuzz.convert(x)
                .exists(s -> validStringsFor(x).contains(s));
    }

    private static Arbitrary<Integer> validInput() {
        return integer().filter(x -> x >= MIN && x <= MAX);
    }

    private static Seq<String> validStringsFor(Integer x) {
        return fizzBuzzStrings.append(x.toString());
    }

    private static Arbitrary<Integer> invalidInput() {
        return integer().filter(x -> x < MIN || x > MAX);
    }
}