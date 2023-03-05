import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{

  title = 'Datting App';
  users: any;

  constructor(private http:HttpClient){

  }
  
  ngOnInit(): void {
    this.http.get("https://localhost:6001/api/users").subscribe({
      next: (v: Object) => {
        console.log(v),
        this.users = v;
      },
      error: (err: any) => console.log('Deu erro na chamada ajax: ', err),
      complete: () => console.log('Http Request complete')
    });
  }

  
}
