import { VideoPokerType } from '../new-game/video-poker-type';

export interface GameVarsModel {
    videoPokerType: VideoPokerType;
    unitSize: number;
    betSize: number;
    money: number;
}
