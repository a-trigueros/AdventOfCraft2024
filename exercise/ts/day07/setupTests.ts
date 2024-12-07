import { Gift } from "./src/workshop/gift";
import { Status } from "./src/workshop/status";

expect.extend({
  toBeProduced(received: Gift) {
    const pass = received?.getStatus() === Status.Produced;
    const maybeName = received?.getName() ?? "gift"
    if (pass) {
      return {
        message: () => `expected ${maybeName} not to be produced`,
        pass: true,
      };
    } else {
      return {
        message: () => `expected ${maybeName} to be produced`,
        pass: false,
      };
    }
  },
});