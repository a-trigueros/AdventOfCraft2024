import 'jest';
import { Gift } from '../../src/workshop/gift';
import { Status } from '../../src/workshop/status';

declare global {
  namespace jest {
    interface Matchers<R> {
      toBeProduced(): R;
    }
  }
}
