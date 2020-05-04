import { VideoPokerType } from './video-poker-type';

export interface NewGameModel {
    playMoney: number,
    unitSize: number,
    videoPokerType: VideoPokerType.JacksOrBetter
}
