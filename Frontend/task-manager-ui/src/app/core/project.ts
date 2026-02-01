import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class ProjectService {
  private api = 'http://localhost:7075/api/projects';

  constructor(private http: HttpClient) {}

  getProjects() {
    return this.http.get<any[]>(this.api);
  }
}

