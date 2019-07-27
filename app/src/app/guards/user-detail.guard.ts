import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {UsersService} from "../services/users/users.service";

@Injectable({
  providedIn: 'root'
})
export class UserDetailGuard implements CanActivate {

  constructor(private router: Router, private users: UsersService) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let id = +route.url[1].path;
    if (isNaN(id) || id < 1) {
      window.alert('user does not exists');
      this.router.navigate(['/users']);
      return false;
    }

    if (!this.users.get(id)) {
      window.alert('user does not exists');
      this.router.navigate(['/users']);
      return false;
    }

    return true;
  }
}
