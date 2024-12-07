import { Gift } from "../../src/workshop/gift";
import { Workshop } from "../../src/workshop/workshop";


export const givenAnEmptyWorkshop = () => new WorkshopBuilder

export class WorkshopBuilder {
    private toyNames: string[] = [];

    public thatProduces(toyName: string): WorkshopBuilder {
        this.toyNames.push(toyName);
        return this;
    }

    public build(): Workshop {
        var workshop = new Workshop();
        this.toyNames.forEach(toyName => workshop.addGift(new Gift(toyName)));
        return workshop;
    }
}