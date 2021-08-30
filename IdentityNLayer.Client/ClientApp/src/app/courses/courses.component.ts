import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html'
})
export class CoursesComponent {
  public courses: Course[];
  public checkFilter: boolean = false
  private tempCourses: Course[];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Course[]>(baseUrl + 'courses').subscribe(result => {
      this.tempCourses = this.courses = result;
    }, error => console.error(error));
  }
  filterStartDate(startdate: Date) {
    this.courses = this.tempCourses.filter(cr => cr.startDate >= startdate);
    this.checkFilter = true;
  }
  removeFilter() {
    this.checkFilter = false;
    this.courses = this.tempCourses;
  }
}

interface Course {
  title: string;
  description: string;
  startDate: Date;
}
