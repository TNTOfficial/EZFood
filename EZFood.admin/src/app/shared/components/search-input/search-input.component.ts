import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter, OnInit, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, ReactiveFormsModule, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './search-input.component.html',
  styleUrl: './search-input.component.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SearchInputComponent),
      multi: true
    }
  ]
})
export class SearchInputComponent implements ControlValueAccessor, OnInit {
  @Input() items: any[] = [];
  @Input() placeholder: string = '';
  @Output() search = new EventEmitter<string>();
  @Output() select = new EventEmitter<any>();


  inputValue: string = '';
  showSuggestions: boolean = false;
  filteredItems: any[] = [];
  highlightedIndex: number = -1;

  private onChange = (value: any) => { }
  private onTouched = () => { };

  ngOnInit(): void {
    this.filteredItems = this.items;
  }

  writeValue(value: any): void {
    this.inputValue = value;
    this.filterItems();
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  onSearch(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.inputValue = target.value;
    this.filterItems();
    this.search.emit(this.inputValue);
  }

  onBlur(): void {
    setTimeout(() => {
      this.showSuggestions = false;
      this.onTouched();
    }, 200);
  }

  onSelect(item: any): void {
    this.inputValue = item.name;
    this.showSuggestions = false;
    this.onChange(item.name);
    this.select.emit(item);
  }

  onKeyDown(event: KeyboardEvent): void {
    switch (event.key) {
      case 'ArrowDown':
        this.highlightedIndex = (this.highlightedIndex + 1) % this.filteredItems.length;
        break;
      case 'ArrowUp':
        this.highlightedIndex = (this.highlightedIndex - 1 + this.filteredItems.length) % this.filteredItems.length;
        break;
      case 'Enter':
        if (this.highlightedIndex >= 0 && this.highlightedIndex < this.filteredItems.length) {
          this.onSelect(this.filteredItems[this.highlightedIndex]);
        }
        break;
      default:
        break;
    }
  }

  filterItems(): void {
    this.filteredItems = this.items.filter((item) =>
      item?.name?.toLowerCase().includes(this.inputValue.toLowerCase()));
    this.showSuggestions = this.filteredItems.length > 0;
    if (this.filteredItems.length > 0) {
      this.highlightedIndex = 0;
    } else {
      this.highlightedIndex = -1;
    }
  }
}
