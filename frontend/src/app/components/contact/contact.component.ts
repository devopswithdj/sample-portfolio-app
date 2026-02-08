import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, Contact } from '../../services/api.service';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  contact: Contact = {
    name: '',
    email: '',
    message: ''
  };
  
  submitting = false;
  submitted = false;
  error = '';

  constructor(private apiService: ApiService) {}

  onSubmit() {
    if (!this.contact.name || !this.contact.email || !this.contact.message) {
      this.error = 'Please fill in all fields';
      return;
    }
    
    this.submitting = true;
    this.error = '';
    
    this.apiService.submitContact(this.contact).subscribe({
      next: () => {
        this.submitted = true;
        this.submitting = false;
        this.contact = { name: '', email: '', message: '' };
        
        setTimeout(() => {
          this.submitted = false;
        }, 5000);
      },
      error: (err) => {
        this.error = 'Failed to send message. Please try again.';
        this.submitting = false;
        console.error(err);
      }
    });
  }
}
