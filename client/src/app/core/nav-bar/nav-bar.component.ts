import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  constructor(public accountService: AccountService) {}
  isIconHovered: boolean = false;
  isFirstButtonHovered: boolean = false;
  isSecondButtonHovered: boolean = false;

  changeFontSize(increase: boolean) {
    const root = document.documentElement;
    const currentFontSize = window
      .getComputedStyle(root)
      .getPropertyValue('--base-text-font-size');

    if (currentFontSize) {
      const parsedFontSize = parseFloat(currentFontSize);
      const unit = currentFontSize.replace(parsedFontSize.toString(), '');

      let newFontSize: string;

      if (increase) {
        newFontSize = `${parsedFontSize + 2}${unit}`;
      } else {
        newFontSize = `${parsedFontSize - 2}${unit}`;
      }

      root.style.setProperty('--base-text-font-size', newFontSize);
    }
  }

  toggleIconHover(isFirstButton: boolean) {
    if (isFirstButton) {
      this.isFirstButtonHovered = true;
    } else {
      this.isSecondButtonHovered = true;
    }
    this.isIconHovered = true;
  }

  resetIconHover() {
    this.isIconHovered = false;
    this.isFirstButtonHovered = false;
    this.isSecondButtonHovered = false;
  }
}
