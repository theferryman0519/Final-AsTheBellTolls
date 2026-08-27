---
Title: Systems / Economy System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- The currency used throughout *As The Bell Tolls* is called Bellnotes.
- Bellnotes are represented as whole numbers and do not use fractional values.
- When starting a new game, the player begins with 500 Bellnotes.
- The Economy System owns the player's Bellnote balance and the rules used when Bellnotes are earned, spent, deducted, or modified.
- Authored purchase prices, sale prices, and Regency Tax prices are maintained within `ShopSalePrices.md`.
- Individual item value prices remain defined within their respective item data files.
- The Economy System defines how those authored values are interpreted and applied during gameplay.
- Economic balance values may be adjusted during prototyping and playtesting without changing the underlying transaction rules.

---

## Bellnote Balance

- The player's Bellnote balance is stored as a whole-number value.
- The player may earn Bellnotes without a standard gameplay maximum, subject to the technical maximum supported by the save data and user interface.
- Normal purchases cannot reduce the player's Bellnote balance below 0.
- If the player does not have enough Bellnotes to complete a normal purchase, the transaction is rejected.
- A balance of 0 does not prevent the player from earning Bellnotes, receiving rewards, or selling items.
- The only intended method for the player's Bellnote balance to become negative is through a required loan repayment when the player does not have enough Bellnotes to cover the payment.
- While the player's Bellnote balance is negative, normal purchases are disabled.
- Bellnotes earned while the balance is negative are applied normally and first move the balance back toward 0.
- Normal purchasing becomes available again once the player has enough Bellnotes to pay for the requested transaction.

---

## Economy Responsibilities

The Economy System is responsible for:

- Tracking the player's current Bellnote balance.
- Validating whether a Bellnote transaction can be completed.
- Adding Bellnotes earned from approved income sources.
- Deducting Bellnotes from approved spending sources.
- Supporting negative balances caused by required loan repayments.
- Applying shop sale modifiers when applicable.
- Applying the Regency Tax when applicable.
- Returning the final transaction amount before a purchase is confirmed.
- Publishing Bellnote balance changes for the UI and other interested systems.
- Recording lifetime Bellnotes earned and spent when required by the Ledger of Achievements.
- Preventing unauthorized or duplicated transactions.

The Economy System is not responsible for:

- Determining shop inventory.
- Determining whether an item is currently available for purchase.
- Determining an item's authored base purchase price.
- Determining an item's authored base value price.
- Determining relationship progression.
- Determining when a relationship sale becomes unlocked.
- Determining when the Regency Tax becomes active or is removed from an individual shop.
- Determining quest, festival, restoration, or story progression.

Those systems provide the relevant transaction context to the Economy System.

---

## Income Sources

Bellnotes may be earned through approved gameplay sources, including:

- Selling items through the Consumer Bin.
- Selling items directly through any system that explicitly supports direct sales.
- Quest rewards.
- Daily request rewards.
- Main story rewards.
- Festival rewards.
- Grand Showcase rewards.
- NPC rewards.
- Collection or progression rewards.
- Trade-related rewards.
- Other one-time rewards specifically authored to grant Bellnotes.

Each income source determines the amount to award before requesting that the Economy System add the Bellnotes.

---

## Spending Sources

Bellnotes may be spent through approved gameplay sources, including:

- Shop purchases.
- Seeds and farming supplies.
- Animals.
- Ingredients and gathering resources.
- Crafting and fabrication materials.
- Clothing and accessories.
- Tools or tool-related services.
- Recipes or other unlockable goods.
- Restoration-related purchases.
- Invention-related purchases.
- Guest room or other paid services.
- Loan repayments.
- Other explicitly authored services or progression costs.

All normal spending must be validated by the Economy System before the Bellnotes are removed.

---

## Transactions

Every Bellnote transaction should contain enough information to identify:

- Transaction Type.
- Source.
- Base Amount.
- Applied Sale Modifier, if any.
- Applied Tax Modifier, if any.
- Final Amount.
- Whether the transaction adds or removes Bellnotes.

Recommended transaction types include:

- Income
- Purchase
- Sale
- Reward
- Service
- Loan
- Loan Repayment
- Adjustment

The Economy System should resolve the final Bellnote amount before changing the player's balance.

A transaction should only be applied once.

If a transaction cannot be completed, the player's Bellnote balance and involved inventory should remain unchanged.

---

## Purchase Prices

- Base shop purchase prices are authored within `ShopSalePrices.md`.
- A shop purchase may be modified by an eligible relationship sale.
- A shop purchase may be modified by the Regency Tax.
- When both a sale and Regency Tax apply, the sale is calculated first and the Regency Tax is calculated from the resulting sale price.
- Sale and tax modifiers are multiplicative rather than additive.

### Purchase Formula

Without modifiers:

`Final Purchase Price = Base Purchase Price`

With a sale:

`Sale Price = Base Purchase Price × Sale Multiplier`

With Regency Tax:

`Taxed Price = Current Purchase Price × Regency Tax Multiplier`

With both:

`Final Purchase Price = (Base Purchase Price × Sale Multiplier) × Regency Tax Multiplier`

Each completed pricing step is rounded to the nearest whole Bellnote using midpoint rounding away from zero.

### Example

For an item with a Base Purchase Price of 1,000 Bellnotes:

- Base Price: 1,000
- 25% Sale: 750
- 35% Regency Tax without a sale: 1,350
- 25% Sale followed by 35% Regency Tax: 1,013

The sale still meaningfully reduces the player's price while the Regency Tax remains a visible economic consequence.

---

## Relationship Sales

- Certain NPC Connection rewards may unlock permanent sales for the shop associated with that NPC.
- The Relationship System determines whether the player has unlocked a sale.
- The Commerce System provides the applicable sale modifier when requesting a purchase price.
- The Economy System applies the modifier to the base purchase price.
- Relationship sales do not directly alter the authored Base Purchase Price.
- Relationship sales remain beneficial while the Regency Tax is active.
- Unless a specific shop or reward states otherwise, multiple relationship sale modifiers do not stack with one another.
- If more than one permanent sale could affect the same purchase, the highest eligible sale should be used unless a specific design rule states otherwise.

---

## Regency Tax

- The Regency Tax is a consequence that may be applied through the Repossession System.
- The Repossession System determines when the Regency Tax becomes active.
- The current intended Regency Tax rate is 35%.
- The Regency Tax Multiplier is therefore `1.35`.
- The Regency Tax applies to applicable purchases made from affected Blackmere shops.
- The Regency Tax does not change the authored Base Purchase Price.
- The tax is calculated at transaction time.
- If a shop has an active relationship sale, the sale is applied before the Regency Tax.
- The tax remains active for an affected shop until the requirements for removing the tax from that shop are satisfied.
- Removing the Regency Tax from one shop does not automatically remove it from other affected shops unless specified by the Repossession System.
- The Regency Tax should be shown clearly to the player before purchase confirmation.
- Shop interfaces should distinguish the Base Price, any applicable Sale, Regency Tax, and Final Price when necessary for clarity.
- The Regency Tax does not reduce the Bellnotes earned when the player sells an item unless another system explicitly defines a separate sales tax.

---

## Item Selling

- Each sellable item has an authored Value Price.
- The Value Price represents the item's base selling value before applicable quality modifiers.
- The final sell value is calculated by the system responsible for the sale using the item's Value Price and any valid quality modifier.
- The Regency Tax does not modify item sell values unless explicitly changed by the Repossession System.
- Relationship shop sales affect purchase prices only and do not increase item sell values.
- An item may only be sold if its item data allows selling.
- Quest items, key items, or other protected items may be marked as unsellable.

### Quality Sell Multipliers

When an item supports quality, its Value Price may be modified by its current quality.

The canonical quality multipliers are defined by the Item System.

The Economy System receives the resolved sell value or the information required to calculate it consistently.

The final sell value is rounded to the nearest whole Bellnote using midpoint rounding away from zero.

---

## Consumer Bin

- Items placed within the Consumer Bin are sold according to the Consumer Bin rules.
- The Consumer Bin should determine which submitted items are eligible for sale.
- Eligible items are converted into Bellnotes during the configured end-of-day sale process.
- The Economy System adds the completed sale total to the player's Bellnote balance.
- The sale should be recorded toward lifetime Bellnotes earned where applicable.
- Items should not be removed permanently from the player's ownership until the sale transaction has been successfully resolved.
- The Regency Tax does not reduce Consumer Bin income unless specifically changed by the Repossession System.

---

## Loans

- Loans are obtained through Blackmere Bank & Exchange.
- Loan availability, loan tiers, repayment schedules, and other loan-specific rules are owned by the Loan or Banking System.
- Receiving a loan adds the approved Bellnote amount to the player's balance.
- A loan is not treated as lifetime earned income for Ledger statistics unless the Ledger explicitly tracks borrowed Bellnotes separately.
- Required loan repayments deduct Bellnotes from the player's balance.
- Loan repayments are the only standard gameplay transaction allowed to reduce the player's Bellnote balance below 0.
- If a required repayment exceeds the player's current balance, the full payment is still deducted and the remaining balance becomes negative.
- While the balance is negative, normal purchases are unavailable.
- Selling items and receiving Bellnotes continue to function normally while the player is in debt.
- Bellnotes earned while in debt naturally reduce the negative balance.
- The Repossession System may inspect the player's current outstanding loan state as part of an assessment.
- Historical use of a loan should not be treated as an automatic permanent economic failure unless explicitly required by the Repossession System.

---

## Restoration and Economy

- Restoration materials may be gathered, crafted, fabricated, purchased, or otherwise acquired through their respective systems.
- The Economy System does not directly determine restoration requirements.
- Purchasing restoration materials uses the same purchase validation and pricing rules as other shop transactions.
- If the Regency Tax is active at a shop selling restoration materials, the tax applies to those purchases while that shop remains affected.
- Using Chimes to Advance a restoration timer does not replace Bellnote costs or material requirements.
- Time Manipulation affects eligible waiting durations rather than purchase prices.

---

## Inventions and Economy

- Invention ingredients and fabricated components may have Bellnote opportunity costs even when the player gathers or crafts them directly.
- Purchasing invention materials uses standard purchase rules.
- Time Manipulation may reduce eligible invention timers but does not reduce Bellnote costs or ingredient requirements.
- Inventions that improve gathering, farming, fishing, husbandry, or other resource systems may indirectly increase the player's earning potential.
- Economy balance testing should account for these throughput improvements when evaluating later-game income.

---

## Economic Balance Goals

The economy should support the following player experience:

- The player should regularly make meaningful spending choices without feeling unable to participate in core gameplay.
- Basic necessities should remain reasonably attainable during normal progression.
- Restoration, inventions, animals, clothing, and other larger purchases should create medium- or long-term savings goals.
- Optional cosmetic purchases should compete with progression spending without becoming mandatory.
- Relationship sales should feel materially rewarding.
- The Regency Tax should be clearly felt as a consequence while remaining recoverable through continued town restoration.
- Loans should function as a strategic or recovery tool rather than an automatic requirement.
- No single repeatable activity should become so profitable that it invalidates the value of the game's other economic activities.
- Chime use should improve efficiency without becoming an unlimited Bellnote-generation strategy.
- Later-game inventions may increase earning efficiency, but later-game costs and optional goals should continue to provide useful Bellnote sinks.

---

## Balance Metrics

During prototyping and balancing, track at minimum:

- Average Bellnotes earned per day.
- Median Bellnotes earned per day.
- Average Bellnotes spent per day.
- Average Bellnote balance by season and year.
- Highest-performing repeatable income source.
- Lowest-performing repeatable income source.
- Average number of gameplay days required to afford common purchases.
- Average number of gameplay days required to afford major purchases.
- Percentage of player income spent on required progression.
- Percentage of player income spent on optional purchases.
- Average loan frequency.
- Average time required to repay a loan.
- Frequency and duration of negative Bellnote balances.
- Economic impact of each major invention tier.
- Economic impact of Chime usage.
- Economic impact of relationship sales.
- Economic impact of the Regency Tax.

These metrics should be used to adjust authored prices and rewards rather than changing the fundamental transaction rules unless testing reveals a systemic issue.

---

## Pricing Data Ownership

- `EconomySystem.md` owns economy behavior, transaction rules, formulas, and balance goals.
- `ShopSalePrices.md` owns authored shop purchase prices and reference sale/tax prices.
- Individual item data files own authored item Value Prices.
- Relationship data owns sale unlock requirements.
- The Repossession System owns Regency Tax activation and removal requirements.
- Loan or Banking data owns loan tiers and repayment terms.
- The Ledger System owns lifetime economic statistics presented to the player.

No individual gameplay system should independently calculate a different version of the same purchase, sale, or tax formula.

---

## Save Data

The Economy save data should contain or support:

- Current Bellnote Balance.
- Lifetime Bellnotes Earned.
- Lifetime Bellnotes Spent.
- Lifetime Bellnotes Borrowed, if tracked.
- Lifetime Bellnotes Repaid, if tracked.
- Any economy state that cannot be reconstructed safely from its owning systems.

Relationship sale unlocks, active Regency Tax state, and individual loan state should remain within their owning domain save data unless the final save architecture intentionally centralizes them.

---

## Events

Recommended Economy events include:

- BellnotesChanged
- BellnotesEarned
- BellnotesSpent
- PurchaseSucceeded
- PurchaseFailed
- ItemSold
- LoanReceived
- LoanRepaymentProcessed
- NegativeBalanceEntered
- NegativeBalanceCleared

Events should announce completed state changes rather than being used as substitutes for validated transaction requests.

---

## Administration and Testing

The Admin domain should be able to:

- Set the player's Bellnote balance.
- Add Bellnotes.
- Remove Bellnotes.
- Force a negative Bellnote balance.
- Simulate a purchase.
- Simulate a sale.
- Toggle or simulate the Regency Tax through the appropriate owning system.
- Simulate relationship sale modifiers through the appropriate owning system.
- Inspect the resolved Base Price, Sale Price, Tax Amount, and Final Price.
- Inspect lifetime Bellnote statistics.
- Reset economic testing state.

Whenever possible, Admin actions should use the same Economy APIs as normal gameplay so testing exercises production logic rather than bypassing it.
