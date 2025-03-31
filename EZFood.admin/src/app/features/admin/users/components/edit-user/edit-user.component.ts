import { CommonModule } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import {
  CalendarDays,
  ChevronDown,
  ContactRound,
  CreditCard,
  Earth,
  HandCoins,
  IdCard,
  Landmark,
  LocateFixed,
  LucideAngularModule,
  Mail,
  Map,
  MapPinHouse,
  MapPinned,
  MapPinPlus,
  Moon,
  Phone,
  Sun,
  TextCursor,
  UserCheck,
} from 'lucide-angular';
import { finalize } from 'rxjs';
import { SearchableDropdownComponent } from '../../../../../shared/components/searchable-dropdown/searchable-dropdown.component';
import { ToastContainerComponent } from '../../../../../shared/components/toast-container/toast-container.component';
import { State } from '../../../../../shared/models/state/state.model';
import { City } from '../../../../../shared/models/city/city.model';
import { Relation } from '../../../../../shared/models/relation/relation.model';
import { UserDetail } from '../../../../../shared/models/user/user-detail.model';
import { UserService } from '../../../../../core/services/user/user.service';
import { StateService } from '../../../../../core/services/state/state.service';
import { CityService } from '../../../../../core/services/city/city.service';
import { AuthService } from '../../../../../core/services/auth/auth.service';
import { ToastService } from '../../../../../core/services/common/toast/toast.service';
import { RELATIONS } from '../user-form/data/relation-data';
import { UserUpdate } from '../../../../../shared/models/user/user-update-model';
import { Bank } from '../../../../../shared/models/bank/bank.model';
import { BANKS } from '../user-form/data/banks-list';

@Component({
  selector: 'edit-user',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LucideAngularModule,
    SearchableDropdownComponent,
    ToastContainerComponent
  ],
  templateUrl: './edit-user.component.html',
})
export class EditUserComponent implements OnInit {
  // Lucide Icons
  Sun = Sun;
  Moon = Moon;
  UserCheck = UserCheck;
  ContactRound = ContactRound;
  ChevronDown = ChevronDown;
  TextCursor = TextCursor;
  Phone = Phone;
  Mail = Mail;
  IdCard = IdCard;
  HandCoins = HandCoins;
  MapPinHouse = MapPinHouse;
  Map = Map;
  MapPinned = MapPinned;
  MapPinPlus = MapPinPlus;
  Earth = Earth;
  LocateFixed = LocateFixed;
  Landmark = Landmark;
  CreditCard = CreditCard;
  CalendarDays = CalendarDays;

  // Form and data variables
  profileForm!: FormGroup;
  states: State[] = [];
  cities: City[] = [];
  relations: Relation[] = []
  banks: Bank[] = [];
  loading = signal(false);
  saving = signal(false);
  error = signal('');
  success = signal('');
  userDetail: UserDetail | null = null;
  userId:string | null = ''
  // Dropdown options
  relationOptions = ['S/O', 'D/O', 'W/O', 'C/O', 'H/O'];
  titleOptions = ['Mr.', 'Mrs.', 'Ms.', 'Dr.'];
  professionOptions = [
    'Employed',
    'Unemployed',
    'Professional',
    'Student',
    'Businessman',
    'House Wife',
  ];

  // Dependency injection
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private userService = inject(UserService);
  private stateService = inject(StateService);
  private cityService = inject(CityService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private toastService = inject(ToastService);

  ngOnInit(): void {
    this.relations = RELATIONS;
    this.banks = BANKS;
    this.initForm();
    this.loadStates();
    this.loadUserProfile();

    // Loading cities when state selection changes
    this.profileForm.get('stateId')?.valueChanges.subscribe((stateId) => {
      if (stateId) {
        this.loadCitiesByState(stateId);
      } else {
        this.cities = [];
      }
    });
  }

  initForm(): void {
    this.profileForm = this.fb.group({
      // Personal Details
      nameTitle: ['Mr.', Validators.required],
      name: ['', [Validators.required, Validators.maxLength(60)]],
      relation: ['S/O', Validators.required],
      guardianName: [''],
      dateOfBirth: [''],
      gender: ['', Validators.required],
      email: ['', Validators.email],
      phoneNumber: [
        '',
        [Validators.required, Validators.pattern('^[0-9]{10}$')],
      ],
      martialStatus: [''],
      profession: [''],
      voterNumber: [''],
      gstNumber: [''],

      // Address Details
      address: ['', Validators.required],
      stateId: ['', Validators.required],
      cityId: ['', Validators.required],
      country: ['India', Validators.required],
      pinCode: ['', [Validators.required, Validators.pattern('^[0-9]{6}$')]],

      // Read-only fields (disabled)
      userCode: [{ value: '', disabled: true }],
      sponsorId: [{ value: '', disabled: true }],
      placementId: [{ value: '', disabled: true }],
      sponsorUserName: [{ value: '', disabled: true }],
      placementUserName: [{ value: '', disabled: true }],
      registrationType: [{ value: '', disabled: true }],

      // Bank details (disabled)
      bankAccount: this.fb.group({
        bankName: [''],
        branchName: [''],
        accountNumber: ['', Validators.pattern('^[0-9]{9,18}$')],
        ifscCode: ['', Validators.pattern('^[A-Z]{4}0[A-Z0-9]{6}$')],
        panNumber: ['', Validators.pattern('^[A-Z]{5}[0-9]{4}[A-Z]{1}$')],
        aadharNumber: ['', Validators.pattern('^[0-9]{12}$')],
      }),
      coApplicant: this.fb.group({
        nomineeName: [''],
        relation: [''],
        dateOfBirth: [null],
      }),
    });
  }

  loadStates(): void {
    this.stateService.getAllStates().subscribe({
      next: (states) => (this.states = states),
      error: (err) => console.error('Failed to load states', err),
    });
  }

  loadCitiesByState(stateId: number): void {
    this.cityService.getCitiesByStateId(stateId).subscribe({
      next: (cities) => (this.cities = cities),
      error: (err) =>
        console.error(`Failed to load cities for state ${stateId}`, err),
    });
  }

  loadUserProfile(): void {
    this.loading.set(true);
    this.userId = this.route.snapshot.paramMap.get("id");

    if (!this.userId) {
      this.error.set('User ID is missing');
      this.loading.set(false);
      return;
    }

    this.userService
      .getUserDetails(this.userId)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (user) => {
          this.userDetail = user;
          this.populateForm(user);
        },
        error: (err) => {
          console.error('Error loading user profile', err);
          this.error.set('Failed to load user profile. Please try again.');
        },
      });
  }


  populateForm(user: UserDetail): void {
    // Determine relation and guardian name
    let relation = 'S/O';
    let guardianName = '';

    if (user.fatherName) {
      relation = user.gender === 'Female' ? 'D/O' : 'S/O';
      guardianName = user.fatherName;
    } else if (user.husbandName) {
      relation = user.gender === 'Female' ? 'W/O' : 'H/O';
      guardianName = user.husbandName;
    }

    // First set the state to ensure cities are loaded
    if (user.city?.state?.id) {
      this.loadCitiesByState(user.city.state.id);
    }

    // Use setTimeout to ensure cities are loaded before setting cityId
    setTimeout(() => {
      this.profileForm.patchValue({
        nameTitle: this.determineTitleFromGender(user.gender),
        name: user.name,
        relation: relation,
        guardianName: guardianName,
        dateOfBirth: user.dateOfBirth ? new Date(user.dateOfBirth).toISOString().split('T')[0] : null,
        gender: user.gender,
        email: user.email,
        phoneNumber: user.phoneNumber,
        martialStatus: user.martialStatus,
        profession: user.profession || '',
        voterNumber: user.voterNo || '',
        gstNumber: user.gstNo || '',
        address: user.address,
        stateId: user.city?.state?.id,
        cityId: user.city?.id, // This should now work with the delay
        country: user.country,
        pinCode: user.pinCode,
        userCode: user.userCode,
        sponsorId: user.sponsorId,
        placementId: user.placementId,
        sponsorUserName: user.sponsorUserName,
        placementUserName: user.placementUserName,
        registrationType: user.registrationType,
      });
    }, 300);

    // Populate bank details if available
    if (user.bankAccount) {
      this.profileForm.get('bankAccount')?.patchValue({
        bankName: user.bankAccount.bankName,
        branchName: user.bankAccount.branchName,
        accountNumber: user.bankAccount.accountNumber,
        ifscCode: user.bankAccount.ifscCode,
        panNumber: user.bankAccount.panNumber,
        aadharNumber: user.bankAccount.aadharNumber,
      });
    }

    // Format co-applicant date properly
    if (user.coApplicant) {
      const dob = user.coApplicant.dateOfBirth ?
        new Date(user.coApplicant.dateOfBirth).toISOString().split('T')[0] :
        null;

      this.profileForm.get("coApplicant")?.patchValue({
        nomineeName: user.coApplicant.nomineeName,
        relation: user.coApplicant.relation,
        dateOfBirth: dob
      });
    }
  }

  determineTitleFromGender(gender: string): string {
    if (!gender) return 'Mr.';
    return gender === 'Female' ? 'Mrs.' : 'Mr.';
  }

  onStateChange(state: any): void {
    // Reset city when state changes
    this.profileForm.get('cityId')?.setValue(null);

    // Load cities for the selected state
    if (state && state.id) {
      this.loadCitiesByState(state.id);
    } else {
      this.cities = [];
    }
  }


  selectBank(bank: Bank): void {
    this.profileForm.get('bankAccount')?.get('bankName')?.setValue(bank.name);
  }

  selectRelation(relation: Relation): void {
    this.profileForm.get('coApplicant')?.get('relation')?.setValue(relation.value);
  }

  openDatePicker(event: MouseEvent) {
    const input = event.target as HTMLInputElement;
    input.showPicker ? input.showPicker() : input.click();
  }

  onSubmit(): void {
    if (this.profileForm.invalid) {
      Object.keys(this.profileForm.controls).forEach(key => {
        const control = this.profileForm.get(key);
        if (control instanceof FormGroup) {
          Object.keys(control.contains).forEach(nestedKey => {
            control.get(nestedKey)?.markAsTouched();
          });
        } else {
          control?.markAllAsTouched()
        }
      });
      return;
    }
    if (!this.userDetail?.id) {
      this.error.set('User ID not found');
      return;
    }

    if (!this.profileForm.dirty) {
      this.toastService.warning("Caution", "You haven't changed anything");
      return;
    }

    // Get form data and remove fields that shouldn't be sent
    const formData = { ...this.profileForm.value };

    // Format Father's/Husband's relation with prefix
    const relation = formData.relation;
    const guardianName = formData.guardianName;

    if (relation && guardianName) {
      if (relation === 'S/O' || relation === 'D/O') {
        formData.fatherName = guardianName;
        formData.husbandName = '';
      } else if (relation === 'H/O' || relation === 'W/O') {
        formData.husbandName = guardianName;
        formData.fatherName = '';
      }
    }

    // Clearing data before sending
    delete formData.sponsorName;
    delete formData.placementName;
    delete formData.relation;
    delete formData.guardianName;
    delete formData.nameTitle;

    if (!formData.fatherName) {
      formData.fatherName = '';
    }
    if (!formData.husbandName) {
      formData.husbandName = '';
    }
    if (!formData.dateOfBirth) {
      formData.dateOfBirth = null;
    }

    if (formData.bankAccount) {
      if (
        !formData.bankAccount.bankName &&
        !formData.bankAccount.branchName &&
        !formData.bankAccount.accountNumber &&
        !formData.bankAccount.ifscCode &&
        !formData.bankAccount.panNumber &&
        !formData.bankAccount.aadharNumber
      ) {
        delete formData.bankAccount;
      }
    }
    // Check if coApplicant exists first, then check its fields
    if (formData.coApplicant) {
      if (
        !formData.coApplicant.nomineeName &&
        !formData.coApplicant.relation &&
        !formData.coApplicant.dateOfBirth
      ) {
        delete formData.coApplicant;
      }
    }


    this.saving.set(true);
    this.error.set('');
    this.success.set('');

    this.userService
      .updateUser(this.authService.currentUser$()!.userId, this.userId!, formData as UserUpdate)
      .pipe(finalize(() => this.saving.set(false)))
      .subscribe({
        next: () => {
          this.toastService.success("Success", 'Profile updated successfully!')
          // Clear success message after 5 seconds
          setTimeout(() => {
            this.success.set('');
          }, 5000);
        },
        error: (err) => {
          console.error('Error updating profile', err);
          this.error.set(err.message || 'Failed to update profile. Please try again.');
        },
      });
  }

  cancel(): void {
    this.router.navigate(['/profile']);
  }

  // Dark mode toggle
  isDarkMode = signal(false);

  toggleDarkMode() {
    this.isDarkMode.update((value) => !value);
    document.documentElement.classList.toggle('dark', this.isDarkMode());
  }
}
