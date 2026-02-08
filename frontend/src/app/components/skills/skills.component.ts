import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService, Skill } from '../../services/api.service';

@Component({
  selector: 'app-skills',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.css']
})
export class SkillsComponent implements OnInit {
  skills: Skill[] = [];
  loading = true;
  error = '';
  
  get categorizedSkills() {
    const categories = new Map<string, Skill[]>();
    this.skills.forEach(skill => {
      if (!categories.has(skill.category)) {
        categories.set(skill.category, []);
      }
      categories.get(skill.category)?.push(skill);
    });
    return Array.from(categories.entries());
  }

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.apiService.getSkills().subscribe({
      next: (data) => {
        this.skills = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load skills';
        this.loading = false;
        console.error(err);
      }
    });
  }
}
