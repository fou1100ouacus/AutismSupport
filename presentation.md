---
title: AutismSupport - AI-Powered Daily Companion
theme: simple
highlight-theme: github
---

# AutismSupport
## AI-Powered Daily Companion for Mothers of Children with Autism

---

# Project Overview

**Purpose**: AI-powered daily companion web app for mothers of children with autism

**Target Audience**: Mothers in Egypt (mobile-first, bilingual Arabic/English)

**Core Principle**: NOT medical software - a daily support tool for emotional encouragement and practical tracking

**Daily Usage Goal**: 2-5 minute quick check-in

**Live URL**: https://autism.runasp.net/swagger/index.html

---

# Problem Statement

## Problems Addressed
- Daily overwhelm without structured tracking
- Maternal isolation and lack of safe community
- Flood of complex medical advice vs. need for simple tools
- No single "daily hub" for quick wins
- Risk of unsafe online spaces

## Opportunity
A daily 2-minute ritual app acting as a supportive friend - track wins, share safely, get gentle tips, connect with other moms

---

# Solution Architecture

## Clean Architecture (Onion Architecture)

### Layers
- **Api Layer**: REST API endpoints, Controllers, Swagger documentation
- **Core Layer**: Business logic, Features, Commands/Queries, Validators
- **Data Layer**: Entities, DTOs, Enums, Domain models
- **Infrastructure Layer**: Repositories, Database context, External services
- **Service Layer**: Business services, Integration services

---

# Technology Stack

| Layer | Technology | Purpose |
|-------|-----------|---------|
| **Backend** | ASP.NET Core 8 (C#) | High-performance web framework |
| **Database** | SQL Server | Enterprise-grade database with JSON support |
| **ORM** | Entity Framework Core 8 | Database operations and migrations |
| **Authentication** | JWT + ASP.NET Identity | Secure token-based authentication |
| **Frontend** | JavaScript + React.js | Modern, responsive UI |
| **API Documentation** | Swagger/OpenAPI | Interactive API documentation |
| **File Storage** | Azure Blob Storage | Video and photo uploads |
| **Background Jobs** | Hangfire | Scheduled tasks and processing |

---

# User Management & Authentication

## Features
- User registration with email/password
- JWT-based authentication with refresh tokens
- Role-based authorization (Mother, Moderator, Admin)
- Mandatory disclaimer acceptance (safety gate)
- Profile management
- Email verification services

## Security
- Password hashing with ASP.NET Identity
- Role-based access control
- Audit logging for all actions

---

# Child Profile Management

## Features
- One child per mother (strict enforcement)
- Required fields: Nickname, Age (Years/Months)
- Optional: Gender, Support Needs Level, Challenges, Strengths
- Visual schedule preferences
- Communication methods
- Profile as gate to all features

## Data Model
- JSON fields for flexible data storage
- One-to-one relationship with User
- Timestamps for tracking updates

---

# Daily Tracking System

## Tracking Categories
- Sleep patterns
- Eating habits
- Meltdowns
- Communication progress
- Positive moments
- Sensory experiences
- Strategies used

## Features
- One entry per day per child
- Pre-populated tags from child profile
- Mother's mood checker (stress level 1-10)
- Calendar view for history
- Weekly/monthly insights

---

# Abilities Assessment System

## Features
- Structured ability tests
- Question-answer format
- Results tracking over time
- Progress visualization
- Comparison with previous results

## Purpose
- Monitor developmental progress
- Identify strengths and areas for support
- Provide data for gentle AI insights

---

# Safe Community Platform

## Features
- Post creation with text and photos
- Comment system
- Reactions (Heart, Hug, Pray, ThumbsUp)
- Report system for inappropriate content
- Moderation queue for content approval

## Safety Layers
- Keyword blocklist filtering
- All posts/comments start as "Pending"
- Human moderation before visibility
- Full audit trail

---

# Content Moderation

## Moderator Capabilities
- View pending posts and comments
- Approve or reject content
- Add moderation notes
- Review reported content
- Access full audit logs

## Automated Safety
- Keyword filtering (regex-based)
- Automatic flagging of suspicious content
- Rate limiting
- CAPTCHA on registration

---

# AI-Powered Motion Analysis

## Features
- Video upload for child motion analysis
- Integration with external AI services
- Analysis of behavioral patterns
- Safe, non-medical insights
- Private video storage with presigned URLs

## Technical
- Azure Blob Storage integration
- Background job processing
- Secure file handling

---

# Database Design

## Key Entities
- **Users**: Authentication and profile data
- **ChildProfile**: Child information and preferences
- **TrackingEntries**: Daily tracking records
- **AbilityTestResults**: Assessment results
- **Posts**: Community posts
- **Comments**: Post comments
- **Reactions**: User reactions
- **ModerationQueue**: Pending content
- **AuditLogs**: System activity logs

## Design Patterns
- JSON columns for flexible data
- Foreign key relationships
- Indexing for performance
- Soft delete support

---

# RESTful API Design

## API Endpoints
- `/api/authentication` - Login, register, refresh token
- `/api/users` - User management
- `/api/children` - Child profile CRUD
- `/api/tracking` - Daily tracking entries
- `/api/abilities` - Ability tests and results
- `/api/posts` - Community posts
- `/api/comments` - Comment management
- `/api/reactions` - Reaction handling
- `/api/moderation` - Content moderation
- `/api/motion-analysis` - Video analysis

## Features
- Swagger documentation at `/swagger`
- Standard HTTP status codes
- Consistent response format
- Input validation
- Error handling middleware

---

# Security & Safety Measures

## Authentication & Authorization
- JWT tokens with expiration
- Refresh token mechanism
- Role-based access control
- Custom authorization filters

## Data Security
- SQL Server encryption at rest
- Secure password hashing
- Presigned URLs for file uploads
- CORS configuration

## Content Safety
- Mandatory disclaimer acceptance
- Keyword blocklist
- Human moderation
- Report system
- Audit logging

---

# Clean Architecture Implementation

## Solution Structure
```
AutismSupport/
├── Api/                    # Presentation Layer
│   ├── Controllers/        # API endpoints
│   └── Program.cs         # Application entry point
├── Core/                   # Business Logic Layer
│   ├── Features/          # Feature modules
│   ├── Behaviors/         # MediatR behaviors
│   └── Filters/           # Custom filters
├── Data/                   # Domain Layer
│   ├── Entities/          # Database entities
│   ├── DTOs/              # Data transfer objects
│   └── Enums/             # Enumerations
├── Infrastructure/         # Infrastructure Layer
│   ├── Repositories/      # Data access
│   ├── Context/           # Database context
│   └── Configurations/    # EF configurations
├── Service/                # Service Layer
│   └── Business services
└── XUnitTest/             # Testing Layer
```

---

# CQRS & MediatR Pattern

## Implementation
- Commands for write operations (Create, Update, Delete)
- Queries for read operations (Get, List, Search)
- MediatR for request/response handling
- Separation of concerns
- FluentValidation for input validation

## Benefits
- Clean separation of read/write operations
- Improved maintainability
- Easier testing
- Better performance optimization

## Example Features
- `CreateChildProfileCommand`
- `GetChildProfileQuery`
- `UpdateTrackingEntryCommand`
- `GetTrackingHistoryQuery`

---

# Complete User Journey

## Phases
1. **Landing Page** - Strong disclaimer + language toggle
2. **Registration** - Email/password signup
3. **Disclaimer Acceptance** - Mandatory safety gate
4. **Child Profile Creation** - One-child rule enforcement
5. **Dashboard** - Daily hub with quick actions
6. **Daily Tracking** - Sleep, eating, meltdowns, etc.
7. **Video Upload** - Optional motion analysis
8. **Resources** - Browse practical resources
9. **Community** - Safe social interaction
10. **Moderation** - Content review (admin side)

---

# Key Development Features

## Best Practices
- Clean Architecture principles
- Dependency Injection
- Repository Pattern
- Unit testing with XUnit
- API versioning
- Global error handling
- Localization support (Arabic/English)
- Swagger documentation
- Database migrations

## Code Quality
- SOLID principles
- Async/await patterns
- LINQ queries
- FluentValidation
- Custom middleware
- Extension methods

---

# Deployment Architecture

## Hosting
- Azure App Service (Windows)
- SQL Server database
- Azure Blob Storage for media
- Environment-specific configurations

## CI/CD Considerations
- Database migrations on startup
- Configuration management
- Health checks
- Logging with Serilog
- Error tracking
- Performance monitoring

## Development
- Local SQL Server development
- Swagger for API testing
- HTTP test files (.http)

---

# Quality Assurance

## Testing Layers
- **Unit Tests**: XUnit for business logic
- **Integration Tests**: API endpoint testing
- **Manual Testing**: HTTP test files for API validation

## Test Coverage
- Command handlers
- Query handlers
- Validators
- Repository methods
- Services

## API Testing
- Swagger UI for interactive testing
- HTTP files for automated testing
- Postman collections (if applicable)

---

# Technical Challenges & Solutions

## Challenge 1: Multi-environment database configuration
**Solution**: Dynamic connection string handling with fallback to SQLite

## Challenge 2: Content moderation at scale
**Solution**: Automated keyword filtering + human moderation queue

## Challenge 3: Secure file uploads
**Solution**: Azure Blob Storage with presigned URLs

## Challenge 4: Bilingual support (Arabic/English)
**Solution**: ASP.NET Core localization with RTL support

## Challenge 5: One-child rule enforcement
**Solution**: Database constraint + API validation

---

# Future Roadmap

## Phase 2 Enhancements
- Push notifications for daily tips
- Advanced analytics dashboard
- Printable resources generation
- Offline support (PWA)
- Video streaming optimization

## Phase 3 Enhancements
- Multi-language expansion
- Advanced AI insights
- Telehealth integration (non-medical)
- Parent education modules
- Professional directory

## Technical Improvements
- Redis caching
- Message queue for background jobs
- Advanced monitoring
- Load testing

---

# Project Statistics

## Codebase Metrics
- **Projects**: 6 (Api, Core, Data, Infrastructure, Service, XUnitTest)
- **Controllers**: 13 API controllers
- **Features**: 7 major feature modules
- **Entities**: 18+ database entities
- **Architecture**: Clean Architecture with CQRS

## API Endpoints: 30+ RESTful endpoints
## Database Tables: 15+ tables
## Test Coverage: Unit tests for core business logic

---

# Conclusion & Impact

## Project Achievements
- ✅ Secure, scalable web application
- ✅ Clean Architecture implementation
- ✅ Comprehensive safety features
- ✅ Bilingual support (Arabic/English)
- ✅ AI-powered insights (safe, non-medical)
- ✅ Active moderation system
- ✅ Mobile-first responsive design

## Impact
- Supporting mothers of children with autism
- Reducing maternal isolation
- Providing daily structure and tracking
- Building safe community connections
- Empowering with practical tools

**Live Demo**: https://autism.runasp.net/swagger/index.html

---

# Questions & Answers

## Thank You

**Contact**: [Your Email]
**GitHub**: [Repository Link]
**Live Demo**: https://autism.runasp.net/swagger/index.html

