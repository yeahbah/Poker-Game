import { HandType } from '../hand-type.enum';

export interface PaySchedule {
    handType: HandType;
    betSize: number,
    paySizeInUnits: number
}
