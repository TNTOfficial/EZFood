import { Component, Input, Output, EventEmitter, OnInit, forwardRef, HostListener, ViewChild, ElementRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-searchable-dropdown',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl:"./searchable-dropdown.component.html",
  styleUrls: [],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SearchableDropdownComponent),
      multi: true
    }
  ]
})
export class SearchableDropdownComponent implements ControlValueAccessor, OnInit {
  @Input() items: any[] = [];
  @Input() placeholder: string = 'Select...';
  @Input() displayProperty: string = 'name';
  @Input() valueProperty: string = 'id';
  @Output() selectionChange = new EventEmitter<any>();
  @ViewChild('searchInput') searchInput!: ElementRef;

  selectedItem: any = null;
  isOpen: boolean = false;
  searchTerm: string = '';
  filteredItems: any[] = [];
  highlightedIndex: number = -1;

  private onChange: any = () => { };
  private onTouched: any = () => { };

  ngOnInit(): void {
    this.filteredItems = [...this.items];
  }

  toggleDropdown(): void {
    this.isOpen = !this.isOpen;
    if (this.isOpen) {
      this.searchTerm = '';
      this.filterItems();
      setTimeout(() => { this.searchInput.nativeElement.focus(), 0 })
    } else {
      this.onTouched();
    }
  }

  filterItems(): void {
    if (!this.searchTerm.trim()) {
      this.filteredItems = [...this.items];
    } else {
      const term = this.searchTerm.toLowerCase();
      this.filteredItems = this.items.filter(item =>
        item[this.displayProperty].toLowerCase().includes(term)
      );
    }
    this.highlightedIndex = this.filteredItems.length > 0 ? 0 : -1;
  }

  selectItem(item: any): void {
    this.selectedItem = item;
    this.isOpen = false;
    this.onChange(item[this.valueProperty]);
    this.selectionChange.emit(item);
  }

  writeValue(value: any): void {
    if (value === null || value === undefined) {
      this.selectedItem = null;
      return;
    }

    // Find the corresponding item object based on the value
    this.selectedItem = this.items.find(item => item[this.valueProperty] === value) || null;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: Event): void {
    const target = event.target as HTMLElement;
    const clickedInside = (event.target as HTMLElement).closest('app-searchable-dropdown');
    if (!clickedInside) {
      this.isOpen = false;
    }
  }

  // Handle keyboard navigation on the trigger element
  onTriggerKeyDown(event: KeyboardEvent): void {
    switch (event.key) {
      case 'ArrowDown':
      case 'Down': // IE/Edge
        event.preventDefault();
        if (!this.isOpen) {
          this.toggleDropdown();
        } else {
          this.highlightNext();
        }
        break;
      case 'ArrowUp':
      case 'Up': // IE/Edge
        event.preventDefault();
        if (this.isOpen) {
          this.highlightPrevious();
        }
        break;
      case 'Enter':
      case ' ':
      case 'Spacebar': // IE/Edge
        event.preventDefault();
        if (!this.isOpen) {
          this.toggleDropdown();
        } else if (this.highlightedIndex >= 0 && this.filteredItems.length > 0) {
          this.selectItem(this.filteredItems[this.highlightedIndex]);
        }
        break;
      case 'Escape':
      case 'Esc': // IE/Edge
        event.preventDefault();
        if (this.isOpen) {
          this.isOpen = false;
        }
        break;
      case 'Tab':
        if (this.isOpen) {
          this.isOpen = false;
        }
        break;
    }
  }

  // Handle keyboard navigation in the search box
  onSearchKeyDown(event: KeyboardEvent): void {
    switch (event.key) {
      case 'ArrowDown':
      case 'Down': // IE/Edge
        event.preventDefault();
        this.highlightNext();
        break;
      case 'ArrowUp':
      case 'Up': // IE/Edge
        event.preventDefault();
        this.highlightPrevious();
        break;
      case 'Enter':
        event.preventDefault();
        if (this.highlightedIndex >= 0 && this.filteredItems.length > 0) {
          this.selectItem(this.filteredItems[this.highlightedIndex]);
        }
        break;
      case 'Escape':
      case 'Esc': // IE/Edge
        event.preventDefault();
        this.isOpen = false;
        break;
    }
  }

  // Highlight the next item in the list
  highlightNext(): void {
    if (this.filteredItems.length === 0) {
      return;
    }

    if (this.highlightedIndex < this.filteredItems.length - 1) {
      this.highlightedIndex++;
    } else {
      // Wrap around to the first item
      this.highlightedIndex = 0;
    }
    this.scrollToHighlighted();
  }

  // Highlight the previous item in the list
  highlightPrevious(): void {
    if (this.filteredItems.length === 0) {
      return;
    }

    if (this.highlightedIndex > 0) {
      this.highlightedIndex--;
    } else {
      // Wrap around to the last item
      this.highlightedIndex = this.filteredItems.length - 1;
    }
    this.scrollToHighlighted();
  }

  // Ensure the highlighted item is visible in the dropdown
  private scrollToHighlighted(): void {
    setTimeout(() => {
      const highlightedElement = document.querySelector(`app-searchable-dropdown li:nth-child(${this.highlightedIndex + 1})`);
      if (highlightedElement) {
        highlightedElement.scrollIntoView({ block: 'nearest' });
      }
    });
  }
}
