import { Component, OnInit } from '@angular/core';
import { VideoPokerService } from '../video-poker.service';
import { PaySchedule } from '../pay-schedule/pay-schedule';
import { CookieService } from 'ngx-cookie-service';
import { VideoPokerType } from '../new-game/video-poker-type';
import { List } from 'linqts';


@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  payschedule: PaySchedule[];  
  betSize: number = 1;
  displayedColumns: string[] = ['pair', 'twoPair', 'threeOfAKind']

  constructor(
    private videoPokerService: VideoPokerService,
    private cookieService: CookieService) { }

  ngOnInit(): void {    

    console.log(this.cookieService.getAll());
    let videoPokerType = VideoPokerType[this.cookieService.get("video-poker-type")];
    let unitSize = Number.parseFloat(this.cookieService.get("video-poker-unitsize"));

    this.videoPokerService
      .getPayschedule(videoPokerType, unitSize, 1)
      .subscribe(data => { 
        this.activePayschedule = data;
        
        // console.log(this.payschedule);
        
        let activePayschedule = this.payschedule
          .Where(pay => pay.betSize == this.betSize)
          .OrderByDescending(o => o.paySizeInUnits)
          .ToArray();
        
        this.initializeActivePayschedule(activePayschedule);

        // console.log(this.activePayschedule);
      });  
  }

  initializeActivePayschedule(activePayschedule: PaySchedule[]) {
    var payData: PayscheduleData[];
    for (let pay in activePayschedule) {
      let paySchedule = activePayschedule[pay];
      let columnName = paySchedule.handType.toString();
      this.displayedColumns.push(columnName);

      //var payData: PayscheduleData[];
      this.activePayschedule.push({
          [columnName]: paySchedule.paySizeInUnits
      });
    }

    console.log(this.activePayschedule)
  }
}
