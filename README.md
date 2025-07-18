
# Concurrency Challenger

This project demonstrates practical concurrency conflict handling in a real-world scenario. It simulates a race condition on updating a product's stock count (inventory) using a .NET Web API and explores two concurrency control strategies:

+ Optimistic Concurrency Control (using RowVersion)
+ Distributed Locking (using Redis)

It also includes a parallel console client to simulate multiple users trying to order the same product at the same time.

## What Does It Demonstrate?
+ Concurrency issues like over-ordering or data corruption.
+ Optimistic concurrency control using EF Core and [Timestamp] RowVersion.
+ Distributed lock strategy using Redis (via RedLock).
+ Real-time logs to show lock behavior, race outcomes, and EF Core concurrency exceptions.

## How to Run
Prerequisites:
+ .NET 9
+ Redis

**Setup & Run**

Clone the repository:
`git clone https://github.com/mgh9/Concurrency-Challenger.git
cd Concurrency-Challenger`


Run Redis via Docker Compose:
`docker compose up -d`


Run the `Web API`

Run the `Tester Console App`

or

Run both of them together (using your IDE's `Multiple Startup Projects` profile)

## How it works?

**Optimistic Concurrency (RowVersion)**

+ Each Product record includes a RowVersion (timestamp).
+ EF Core automatically detects if the row has changed during update.
+ If two users fetch the same product and try to update concurrently, only the first one succeeds.
+ The second update throws a `DbUpdateConcurrencyException` , simulating a real-world race condition.
+ Call `POST /api/orders/{productId}` to simulate this scenario.


**Distributed Lock (Redis-based)**
+ The API endpoint acquires a Redis lock before processing a request.
+ Ensures only one request can manipulate a product at a time.
+ You can test this behavior by calling `POST /api/orders/with-lock/{productId}`.
