import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { of, Observable } from 'rxjs';
import { UserService } from '../../core/user';
import { User } from '../../models/user';
import { CreateUser } from '../../models/create-user';
import { delay } from 'rxjs/operators';

@Component({
  standalone: true,
  selector: 'app-users',
  templateUrl: './users.html',
  styleUrls: ['./users.css'],
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule
  ],
})

export class UserList implements OnInit {
  
  users$: Observable<User[]> = of([]);
  
  displayedColumns = ['name', 'email', 'role', 'actions'];

  isEdit = false;

  createUserForm: CreateUser = {
    name: '',
    email: '',
    password: '',
    role: ''
  };

  editUserForm: User | null = null;

  constructor(
    private userService: UserService,
    private cdr: ChangeDetectorRef 
  ) {}

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.users$ = this.userService.getUsers().pipe(delay(0));
     this.cdr.detectChanges(); 
  }

  createUser() {
    this.userService.createUser(this.createUserForm).subscribe(() => {
      this.resetCreateForm();
      this.loadUsers();
    });
  }

  startEdit(user: User) {
    this.editUserForm = { ...user };
    this.isEdit = true;
  }

  updateUser() {
    if (!this.editUserForm) return;

    this.userService.updateUser(this.editUserForm).subscribe(() => {
      this.cancelEdit();
      this.loadUsers();
    });
  }

  deleteUser(id: number) {
    if (confirm('Delete this user?')) {
      this.userService.deleteUser(id).subscribe(() => this.loadUsers());
    }
  }

  cancelEdit() {
    this.editUserForm = null;
    this.isEdit = false;
  }

  resetCreateForm() {
    this.createUserForm = { name: '', email: '', password: '', role: '' };
  }
}
