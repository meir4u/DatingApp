import { ChangeDetectionStrategy, Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { AdminService } from '../../_services/admin.service';
import { Photo } from '../../_models/photo';
import { take } from 'rxjs';

@Component({
  selector: 'app-photo-management',
  templateUrl: './photo-management.component.html',
  styleUrls: ['./photo-management.component.scss'],
})
export class PhotoManagementComponent implements OnInit {
  photos: Photo[] = [];

  constructor(public adminService: AdminService) { }

  ngOnInit(): void {
    this.getPhotosForApproval();
  }

  getPhotosForApproval() {
    this.adminService.getPhotosForApproval().pipe(take(1)).subscribe({
      next: photos => {
        if (photos) {
          this.photos = photos;
        }
      },
      error: error=>console.log(error),
    });
  }

  approvePhoto(photo: Photo) {
    this.adminService.approvePhoto(photo.id).pipe(take(1)).subscribe({
      next: value => {
        console.log(value);
        this.getPhotosForApproval();
      },
      error: error => console.log(error),
      complete: () => {
        console.log(photo.id);
      }
    });
  }

  rejectPhoto(photo: Photo) {
    this.adminService.rejectPhoto(photo.id).pipe(take(1)).subscribe({
      next: value => {
        (value !== true) ? console.log('rejected seccuess') : console.log('reject failed');
      },
      error: error => console.log(error),
      complete: () => {
        console.log(photo.id);
      }
    });
  }

}
