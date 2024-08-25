import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, from, map } from 'rxjs';
import { switchMap, take, tap } from 'rxjs/operators';
import { User } from '../_models/user';
import { environment } from '../../environments/environment';
import { PresenceService } from './presence.service';
import { loadGapiInsideDOM } from 'gapi-script';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private auth2: any;
  baseUrl = environment.apiUrl;
  private currentUserSourse = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSourse.asObservable();
  
  constructor(
    private http: HttpClient,
    private presenceService: PresenceService,

  ) {
    this.initializeGoogleAPI();
  }

  private initializeGoogleAPI() {
    // Initialize the Google Auth2 library
    loadGapiInsideDOM().then(() => {
      gapi.load('auth2', () => {
        gapi.auth2.init({
          client_id: environment.google.client_id,
          //scope: 'profile email',
          redirect_uri: 'http://localhost:44387/google/auth/callback/' 
        }).then(() => {
          this.auth2 = gapi.auth2.getAuthInstance();
        });
      });
    });
  }

  login(model: User) {
    return this.http.post<User>(this.baseUrl + 'account/login', model)
      .pipe(
        take(1),
        map((response: User) => {
          return response;
        
        }),
        tap((user: User) => {
          if (user) {
            this.setCurrentUser(user);
          }
        }),
      );
  }

  loginGoogle(): Observable<User> {
    return from(this.auth2.signIn()).pipe(
      take(1),
      switchMap((googleUser: any) => {
        const idToken = googleUser.getAuthResponse().id_token;
        //const idToken = googleUser.getAuthResponse().access_token;
        return this.http.post<User>(this.baseUrl + 'account/login/google', { Code: idToken });
      }),
      tap((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      take(1),
      map(user => {
        if (user) {
          this.setCurrentUser(user);
        }
        return user;
      })
    );
  }

  setCurrentUser(user: User):void {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);

    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSourse.next(user);
    this.presenceService.createHubConnection(user);
  }

  logout():void { 
    localStorage.removeItem('user');
    this.currentUserSourse.next(null);
    this.presenceService.stopHubConnection();
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
