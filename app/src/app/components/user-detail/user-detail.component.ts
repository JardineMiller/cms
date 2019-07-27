import {Component, OnInit} from '@angular/core';
import {IUser} from "../../../models/interfaces/user";
import {ActivatedRoute, Router} from "@angular/router";
import {UsersService} from "../../services/users/users.service";

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {

  public pageTitle: string = "User";
  public user: IUser = {id: 1, name: "Jardine", email: "jardine@something.com"};
  public props: string[] = Object.keys(this.user);

  constructor(private router: Router, private route: ActivatedRoute, private users: UsersService) {
    let userId = this.route.snapshot.paramMap.get('id');
    this.user = users.get(parseInt(userId));
  }

  ngOnInit() {

  }

  back(): void {
    this.router.navigate(['/users']);
  }

  saveUser() {
    this.users.update([this.user]).then(
      updatedUser => {
        this.user = updatedUser as IUser;
        this.router.navigate(['/users']);
      }
    )
  }
}
