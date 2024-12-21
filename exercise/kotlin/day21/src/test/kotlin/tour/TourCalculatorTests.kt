package tour

import io.kotest.core.spec.style.StringSpec
import io.kotest.property.Arb
import io.kotest.property.arbitrary.*
import io.kotest.property.forAll

class TourCalculatorTests : StringSpec({
    "should generate arbitrary Steps" {
        val stepArb =  arbitrary {
            val time = Arb.localTime().bind()
            val location = Arb.string(10..12).bind()
            val deliveryTime = Arb.int(21, 150).bind()
            Step(time, location, deliveryTime)
        }

        val listOfStepArb = arbitrary {
            val count =  Arb.int(0, 20).bind()
            val steps = mutableListOf<Step>()
            for (i in 1..count) {
                steps.add(stepArb.next())
            }
            steps
        }

        forAll(listOfStepArb) { steps ->
            LegacyTourCalculator(steps).calculate() == TourCalculator(steps).calculate()
        }
    }

})