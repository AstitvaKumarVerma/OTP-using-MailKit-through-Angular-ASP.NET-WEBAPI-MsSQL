import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  email: string = '';
  password: string = '';

  constructor(private http: HttpClient, private router: Router,private toastr: ToastrService){ }

  registerUser() {

    const userData = {
      Email: this.email,
      Password: this.password,
    };

    this.http.post('https://localhost:7188/api/EmailOtp/Register', userData).subscribe((response: any) => {
      // Handle the response from the .NET Core backend here
      console.log(response.responseMessage);
      if (response.responseMessage == 'Already')
       {
        this.toastr.error('Already Exists!!', 'Email!');
      }
      else 
      {
        this.toastr.success('registered','email')
        this.router.navigate(['/verify']);
      }
    });
  }

}
