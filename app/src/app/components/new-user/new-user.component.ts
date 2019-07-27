import {Component, OnInit} from '@angular/core';
import {IUser} from "../../../models/interfaces/user";
import {ActivatedRoute, Router} from "@angular/router";
import {UsersService} from "../../services/users/users.service";

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.scss']
})
export class NewUserComponent implements OnInit {

  public pageTitle: string = "User";
  public props: string[];
  public user: IUser;

  constructor(private router: Router, private route: ActivatedRoute, private users: UsersService) {
    this.user = {name: "", email: ""};
    this.props = Object.keys(this.user);
  }

  ngOnInit() {

  }

  back(): void {
    this.router.navigate(['/users']);
  }

  validateUserSave(user: IUser) {
    return !(!user.name || !user.email);
  }

  saveUser() {
    console.log('saving');
    let canSave = this.validateUserSave(this.user);
    console.log(canSave);
    console.table(this.user);

    if (!canSave) {
      window.alert('Unable to save worker');
      return;
    }

    this.users.create([this.user as IUser]).then(
      savedUser => {
        this.user = savedUser as IUser;
        this.router.navigate(['/users']);
      }
    )
  }

}
