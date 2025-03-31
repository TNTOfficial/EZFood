import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css'
})
export class PaginationComponent {
  @Input() currentPage: number = 1;
  @Input() totalPages: number = 1;
  @Output() pageChange = new EventEmitter<number>();

  get pages(): number[] {
    const pageArray: number[] = [];
    const totalPagesToShow = 5;
    let startPage = Math.max(1, this.currentPage - Math.floor(totalPagesToShow / 2));
    let endPage = Math.min(this.totalPages, startPage + totalPagesToShow - 1);

    //Adjust startpage if end is maxed out
    if (endPage === this.totalPages) {
      startPage = Math.max(1, endPage - totalPagesToShow + 1);
    }
    for (let i = startPage; i <= endPage; i++) {
      pageArray.push(i);
    }
    return pageArray;
  }
  goToPage(page: number): void {
    if (page !== this.currentPage && page >= 1 && page <= this.totalPages) {
      this.pageChange.emit(page);
    };
  }
}
