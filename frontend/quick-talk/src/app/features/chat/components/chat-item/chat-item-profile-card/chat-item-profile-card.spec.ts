import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatItemProfileCard } from './chat-item-profile-card';

describe('ChatItemProfileCard', () => {
  let component: ChatItemProfileCard;
  let fixture: ComponentFixture<ChatItemProfileCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatItemProfileCard],
    }).compileComponents();

    fixture = TestBed.createComponent(ChatItemProfileCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
