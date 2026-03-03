#  Autism Support

**AI-Powered Daily Support Web App for Mothers of Children with Autism**  

**Primary users:** Mothers in Egypt 

## Core Principle – Repeated on Every Critical Screen

> **This is NOT medical software. It is NOT a substitute for professional therapy, diagnosis, or medical care.**  
> It is a practical daily support tool created with love for mothers — including gentle AI assistance only.  
> The AI chatbot provides general encouragement, ideas, and reflections — never medical advice.  
> Always consult qualified professionals for health, behavior, or developmental concerns.

## 1. Product Overview

**Project Autism** is a secure, **AI-enhanced** web application built exclusively for mothers to support their autistic children through:

- **Mandatory mother account creation** + strong disclaimer acceptance
- One child profile (strict rule)
- Daily manual tracking (sleep, eating, moods, strategies…)
- Private short video uploads of calm moments
- Practical, non-medical resource library
- Heavily moderated emotional support community
- **AI Chatbot** — safe, empathetic companion for daily encouragement, answering simple parenting questions, suggesting calm-down ideas (non-medical, always redirects to professionals when needed)
- **AI-Powered Progress Tracking** — analyzes historical tracking data (external safe AI) → generates gentle, positive text insights & patterns (e.g. "Lately more calm moments on days with visual schedule — great observation! ❤️") — never diagnosis or medical interpretation

**Key Safety & Ethics Rules**

- Strong, repeated disclaimers everywhere — AI is **not** a therapist
- AI chatbot: limited scope, crisis redirection, no false empathy, no medical claims
- All community content: human moderation + keyword blocks
- One child per account
- Quick daily usage (2–5 min core loop)

## 2. Complete User Flow – Deep Version (Mother’s Perspective)

### Phase 0 – Anonymous Visitor
1. Opens website (mobile browser – most likely Egypt)  
   → Strong repeated disclaimer + AR/EN toggle (RTL)  
   → Big button: «ابدئي بدعم طفلي» → Register


### Phase 1 – Account Creation + Disclaimer (Critical Safety Gate)
2. Mother creates account: Email + Password  
3. Forced non-skippable disclaimer screen  
   → Checkbox + "Confirm" (digital signature)


### Phase 2 – Child Profile (Gate to All Features)
4. Redirect → Create one child profile  
   Required: Nickname, AgeYears, AgeMonths  
   Optional: challenges, strengths, preferences…


5. No profile → 403 on dashboard/tracking/community

### Phase 3 – Daily Core Loop
6. Login → Personalized dashboard  
   → Today + «كيف كان اليوم مع [Nickname]؟»  
   → Mother mood picker (1–10 + emoji)

7. Daily Tracking Entry (one per day)
    
8. View History → calendar/list

9. Upload Video (<5 min) → presigned URL + queue

**New: AI Chatbot Access**  
→ Floating or dedicated tab: "Chat with Support Bot"  
→ Safe prompts only (e.g. "Ideas for calm-down after meltdown?")  
→ Responses: empathetic, practical, redirect to pros if serious  
→ Backend: calls external safe AI with strict filters & disclaimers

**New: AI Progress Insights**  
→ Weekly/monthly view in History tab  
→ Button: "See Gentle Patterns"  
→ External AI analyzes tracking history → returns **only safe, positive text** (no diagnosis)  
→ Example: "You've noticed more positive moments when using sensory strategies — keep going! ❤️"

### Phase 4 – Resources
10. Browse categories (Visual Schedules, Mother Self-Care…)

### Phase 5 – Community (Moderated)
11–14. Approved feed → Post (Pending) → Reactions immediate, Comments Pending

### Phase 6 – Admin
15–16. Queue → Approve/Reject → Audit logs

### Phase 7 – Long-term Cycle
Daily check-in + AI chatbot for support + AI gentle insights + daily tip banner

## 3. Main Navigation Summary

Anonymous → Register (Mother Account) → Disclaimer (mandatory)
↓
Create Child Profile (one only)
↓
Dashboard (daily hub)
├── Today’s Tracking Entry
├── View Past Days + AI Gentle Patterns
├── Upload Video Moment
├── AI Chatbot (safe support)
├── Resources Library
└── Community Feed (moderated)
├── Read Approved Posts
├── React / Comment (pending)
└── Create New Post (pending)



## 4. Technical Stack

| Layer              | Technology                        |
|--------------------|-----------------------------------|
| Backend            | ASP.NET Core 8 (C#)               |
| Database           | Microsoft SQL Server              |
| Frontend           | JavaScript + React.js / Next.js   |
| ORM                | Entity Framework Core 8           |
| Auth               | JWT + Refresh tokens              |
| File Storage       | Azure Blob Storage                |
| Background Jobs    | Hangfire                          |
| AI Integration     | External safe AI API (tips, insights, chatbot)             |
| Styling            | Tailwind CSS + DaisyUI            |
| Hosting            | Azure App Service                 |

## 5. AI-Specific Safety & Ethics

- **AI Chatbot** — always shows: "I'm not a doctor or therapist. For serious concerns, contact a professional."
- Redirects crisis/self-harm talk → emergency resources
- No storage of sensitive health data in AI prompts
- Progress insights → positive framing only, no predictions or diagnoses
- Human moderation layer for community remains primary safety net

Created with love for Aya and all incredible mothers in Egypt ❤️

Last updated: March 2026
