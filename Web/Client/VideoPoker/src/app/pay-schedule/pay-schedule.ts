import { HandType } from '../hand-type.enum';

// export interface PaySchedule {
//     handType: HandType;
//     betSize: number,
//     paySizeInUnits: number
// }

export interface PaySchedule {
    pair: number,
    twoPair: number,
    threeOfAKind: number,
    straight: number,
    flush: number,
    fullhouse: number,
    fourOfAKind: number,
    straightFlush: number,
    royalFlush: number
}
