import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/service/users.service';
import { Users } from 'src/app/models/users';

@Component({
  selector: 'app-allusers',
  templateUrl: './allusers.component.html',
  styleUrls: ['./allusers.component.css']
})
export class AllusersComponent implements OnInit {
  user =  {} as Users;
  users!: Users[];

  constructor(private userServices: UsersService) { }

  ngOnInit(): void {
    this.GetAllUser();
  }

  GetAllUser(){
    this.userServices.GetAll().subscribe((users: Users[]) => {
      this.users = users;
      console.log(users);
    });
  }
}
