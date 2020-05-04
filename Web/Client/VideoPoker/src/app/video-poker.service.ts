import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { NewGameModel } from './new-game/new-game-model';
import { Observable, throwError } from 'rxjs';
import { catchError, retry} from 'rxjs/operators';
import { VideoPokerType } from './new-game/video-poker-type';
import { PaySchedule } from './pay-schedule/pay-schedule';

@Injectable({
  providedIn: 'root'
})
export class VideoPokerService {

  constructor(
    private http: HttpClient
  ) { }

  newGame(newGameModel: NewGameModel): Observable<any> {
    console.log(JSON.stringify(newGameModel));

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    
    return this.http.post<NewGameModel>("https://localhost:5001/api/videopoker/newgame", newGameModel, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  getPayschedule(videoPokerType: VideoPokerType, unitSize: number, betSize: number): Observable<PaySchedule[]> {
    let params = new HttpParams().set("videoPokerType", videoPokerType.toString());
    return this.http.get<PaySchedule[]>("https://localhost:5001/api/videopoker/payschedule", 
        {
          headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
          params: params
    });
  }


  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };

}
