import { givenAnEmptyWorkshop } from "./builders/workshopBuilder";

describe('Workshop', () => {
    const TOY_NAME = '1 Super Nintendo';

    it('completes an existing gift and sets its status to produced', () => {

        const workshop = givenAnEmptyWorkshop()
            .thatProduces(TOY_NAME)
            .build();

        const completedGift = workshop.completeGift(TOY_NAME);

        expect(completedGift).toBeProduced();
    });

    it('cannot complete a non-existing gift', () => {
        const workshop = givenAnEmptyWorkshop().build();

        const completedGift = workshop.completeGift('NonExistingToy');

        expect(completedGift).not.toBeProduced()
    });
});

