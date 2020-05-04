import { Component, OnInit } from '@angular/core';
import { FormBuilder, Form, FormGroup } from '@angular/forms';
import { NewGameModel } from './new-game-model';
import { VideoPokerType } from './video-poker-type';
import { Router } from '@angular/router';
import { VideoPokerService } from '../video-poker.service';

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
    private videoPoker: VideoPokerService
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
    let gameParam: NewGameModel = {
      playMoney: formValue.depositAmount,
      unitSize: Number.parseFloat(formValue.unitSize),
      videoPokerType: VideoPokerType.JacksOrBetter
    }
    
    this.videoPoker.newGame(gameParam)
      .subscribe(() => {
        this.router.navigate(['/game']);
      });
  }

}
