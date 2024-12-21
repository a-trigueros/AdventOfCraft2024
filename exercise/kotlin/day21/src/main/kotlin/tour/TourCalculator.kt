package tour

import arrow.core.Either
import arrow.core.left
import arrow.core.right
import java.time.Duration
import java.time.LocalTime

data class Step(val time: LocalTime, val label: String, val deliveryTime: Int)
{
    override fun toString(): String {
        return "${this.time} : ${this.label} | ${this.deliveryTime} sec"
    }
}

class TourCalculator(private var steps: List<Step>) {
    private val failureMessage: String = "No locations !!!"

    fun calculate(): Either<String, String> = if (haveSteps()) {
        buildTour(steps).right()
    } else {
        failureMessage.left()
    }

    private fun haveSteps() = steps.isNotEmpty()

    private fun buildTour(steps: List<Step>): String {

        val stringBuilder = StringBuilder()

        val deliveryTime: Long = appendSteps(steps, stringBuilder)

        stringBuilder.appendLine(toDeliveryTimeString(deliveryTime))

        return stringBuilder.toString()

    }

    private fun toDeliveryTimeString(deliveryTime: Long): String {
        val duration = Duration.ofSeconds(
            deliveryTime
        )
        return "Delivery time | ${duration.toHours()}%02d:${duration.toMinutesPart()}%02d:${duration.toSecondsPart()}%02d"

    }

    private fun appendSteps(steps: List<Step>, stringBuilder: StringBuilder): Long {
        var deliveryTime = 0.0
        steps.sortedBy { it.time }.forEach { s ->
            stringBuilder.appendLine(s)
            deliveryTime += s.deliveryTime
        }
        return deliveryTime.toLong()
    }
}
