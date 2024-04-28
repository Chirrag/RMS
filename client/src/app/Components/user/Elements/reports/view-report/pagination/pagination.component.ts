import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent {
  @Input() itemsPerPage: number;
  @Input() totalItems: number;
  @Output() pageChange: EventEmitter<number> = new EventEmitter<number>();

  currentPage: number = 1;
  maxVisiblePages: number = 5;

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.itemsPerPage);
  }

  get pagesArray(): number[] {
    const startPage = Math.max(1, this.currentPage - Math.floor(this.maxVisiblePages / 2));
    const endPage = Math.min(startPage + this.maxVisiblePages - 1, this.totalPages);

    return Array.from({ length: endPage - startPage + 1 }, (_, i) => startPage + i);
  }

  getDynamicPageRange(): number[] {
    const range = 5;
    const start = Math.max(1, this.currentPage - 2);
    const end = Math.min(this.totalPages, start + range - 1);
    return Array.from({ length: end - start + 1 }, (_, i) => start + i);
  }


  setPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.pageChange.emit(this.currentPage);
    }
  }
}
