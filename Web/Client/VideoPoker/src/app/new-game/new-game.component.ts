import { Component, OnInit } from '@angular/core';
import { FormBuilder, Form, FormGroup } from '@angular/forms';
import { NewGameModel } from './new-game-model';
import { VideoPokerType } from './video-poker-type';
import { Router } from '@angular/router';
import { VideoPokerService } from '../video-poker.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-game-param',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.css']
})
export class NewGameComponent implements OnInit {

  gameParamForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private cookieService: CookieService
  ) {     
    this.gameParamForm = formBuilder.group({
      unitSize: "0.05",
      depositAmount: 100
    });
  }

  ngOnInit(): void {
  }

  onSubmit() {
    let formValue = this.gameParamForm.value;

    // let gameParam: NewGameModel = {
    //   playMoney: Number.parseFloat(formValue.depositAmount),
    //   unitSize: Number.parseFloat(formValue.unitSize),
    //   videoPokerType: VideoPokerType.JacksOrBetter
    // }
    
    this.cookieService.deleteAll();
    this.cookieService.set("video-poker-playmoney", formValue.depositAmount);
    this.cookieService.set("video-poker-unitsize", formValue.unitSize);
    this.cookieService.set("video-poker-type", VideoPokerType.JacksOrBetter.toString());


    this.router.navigate(['/game']);

    // this.videoPoker.newGame(gameParam)
    //   .subscribe(() => {
    //     this.router.navigate(['/game']);
    //   });
  }

}
