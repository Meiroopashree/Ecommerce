import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<string | null>;
  public currentUser: Observable<string | null>;
  public apiUrl = 'https://8080-bfdeeddcedfabcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; 
  private userRoleSubject = new BehaviorSubject<string>('');
  userRole$: Observable<string> = this.userRoleSubject.asObservable();
  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.isAuthenticated());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<string | null>(
      localStorage.getItem('currentUser')
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  register(userName: string, password: string, userRole: string, emailID: string, mobileNumber:string): Observable<any> {
    const body = { userName, password, userRole, emailID, mobileNumber };
    console.log(body);

    return this.http.post<any>(`${this.apiUrl}/auth/register`, body).pipe(
      tap((user) => this.storeUserData(user)),
      catchError(error => {
        if (error.error === "User with that Email already exists") {
          return of(error.error); // Return the error message
        }
        return throwError(error);
      })
    );
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  getUserRole(): Observable<string> {
    return this.userRole$;
  }

  private storeUserData(user: any): void {
    localStorage.setItem('userToken', user.token);
    localStorage.setItem('userRole', user.role);
    localStorage.setItem('user', user.userId.toString());
    console.log('The userID ' + localStorage.getItem('user'));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  login(emailID: string, password: string): Observable<any> {
    const loginData = { emailID, password };
    return this.http.post<any>(`${this.apiUrl}/auth/login`, loginData)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('currentUser', response.username);
          localStorage.setItem('userRole', response.role);
          localStorage.setItem('user', response.userId.toString());

          this.userRoleSubject.next(response.role);
          this.isAuthenticatedSubject.next(true); // Notify observers that the user is authenticated
        })
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    localStorage.removeItem('userRole');
    localStorage.removeItem('email');
    localStorage.removeItem('user');
    this.currentUserSubject.next(null);
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !!token;
  }

  isAdmin(): boolean {
    const role = localStorage.getItem('userRole');
    return role === 'admin' || role === 'ADMIN';
  }

  isUser(): boolean {
    const role = localStorage.getItem('userRole');
    return role === 'user' || role === 'USER';
  }

  getUserId(): number | undefined {
    const userId = localStorage.getItem('user');
    return userId ? +userId : undefined; // Convert to number
  }

  private decodeToken(token: string): any {
    try {
      var decode = JSON.parse(atob(token.split('.')[1]));
      localStorage.setItem('email', decode.sub);
      return decode;
    } catch (error) {
      return null;
    }
  }
}
