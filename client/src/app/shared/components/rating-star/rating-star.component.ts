import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-rating-star',
  templateUrl: './rating-star.component.html',
  styleUrls: ['./rating-star.component.scss'],
})
export class RatingStarComponent {
  @Input() rating = 0; // Ocena od 0 do 5
  stars: { full: boolean; half: boolean }[] = [];

  ngOnChanges() {
    this.stars = this.calculateStars(this.rating);
  }

  private calculateStars(rating: number): { full: boolean; half: boolean }[] {
    const fullStars = Math.floor(rating);
    const hasHalfStar = rating % 1 !== 0;

    return Array.from({ length: 5 }, (_, index) => ({
      full: index < fullStars,
      half: hasHalfStar && index === fullStars,
    }));
  }
}
