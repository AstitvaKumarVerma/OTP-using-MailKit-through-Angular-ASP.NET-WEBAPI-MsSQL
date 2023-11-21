import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})
export class VerificationComponent {
  otp: string='';

  isVerified: boolean = false;
  constructor(private toastr: ToastrService,private route: ActivatedRoute, private http: HttpClient, private router:Router) {
    this.route.queryParams.subscribe((params) => {
      this.otp = params['otp'];
    });
  }

  verifyOTP() {
    const otpData = {
      otp: this.otp,
    };

    this.http.post('https://localhost:7188/api/EmailOtp/VerifyOtp', otpData).subscribe((response:any) => {
      // Handle the response from the .NET Core backend here
      console.log("res",response.verified);
      this.isVerified = response.verified
   
      

      if(this.isVerified==true)
      {
        this.toastr.success('Verified','Email')
      this.router.navigate(['/']);
     }
     else
     {
      this.toastr.error('wrong!!', 'Otp!');

     }
    });
  }

}
